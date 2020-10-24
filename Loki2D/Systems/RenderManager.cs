using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Loki2D.Core.Component;
using Loki2D.Core.Scene;
using Loki2D.Core.Shaders;
using Loki2D.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Loki2D.Systems
{
    public class RenderManager: SystemManager<RenderManager>
    {

        public List<RenderManager> Components = new List<RenderManager>();

        public RenderTarget2D DiffuseTarget;
        public RenderTarget2D NormalTarget;
        public RenderTarget2D ShadowTarget;

        public VertexPositionColorTexture[] Quad;
        public VertexBuffer VertexBuffer;
        private Effect _lightEffect;
        private Effect _lightCombinedEffect;
        private Effect CombinedEffect; 

        public List<Light> Lights = new List<Light>();
        public Color AmbientColor = new Color(.3f, .3f, .3f, 1);
        public float AmbientStrength = 1;
        public float ShadowStrength = 1; 
        
        private Color _ambientLight = new Color(1f, 1f, 1f, 1);
        public float SpecularStrength = 2.0f;

        private EffectTechnique _lightEffectTechniquePointLight;
        private EffectParameter _lightEffectParameterStrength;
        private EffectParameter _lightEffectParameterPosition;
        private EffectParameter _lightEffectParameterConeDirection;
        private EffectParameter _lightEffectParameterLightColor;
        private EffectParameter _lightEffectParameterLightDecay;
        private EffectParameter _lightEffectParameterScreenWidth;
        private EffectParameter _lightEffectParameterScreenHeight;
        private EffectParameter _lightEffectParameterNormapMap;

        private EffectTechnique _lightEffectTechniqueSpotLight;
        private EffectParameter _lightEffectParameterConeAngle;
        private EffectParameter _lightEffectParameterConeDecay;

        private Color _graphicsColor = Color.Black;
        
        public GraphicsDevice GraphicsDevice;
        public RenderManager()
        {
            GraphicsDevice = SceneManagement.Instance.CurrentScene.GraphicsDevice;

            var width = GraphicsDevice.PresentationParameters.BackBufferWidth;
            var height = GraphicsDevice.PresentationParameters.BackBufferHeight;
            DiffuseTarget = new RenderTarget2D(GraphicsDevice, width, height);
            NormalTarget = new RenderTarget2D(GraphicsDevice, width, height);
            ShadowTarget = new RenderTarget2D(GraphicsDevice, width, height);

            Quad = new VertexPositionColorTexture[4];
            Quad[0] = new VertexPositionColorTexture(new Vector3(-1, 1, 0), Color.White, new Vector2(0, 0));
            Quad[1] = new VertexPositionColorTexture(new Vector3(1, 1, 0), Color.White, new Vector2(1, 0));
            Quad[2] = new VertexPositionColorTexture(new Vector3(-1, -1, 0), Color.White, new Vector2(0, 1));
            Quad[3] = new VertexPositionColorTexture(new Vector3(1, -1, 0), Color.White, new Vector2(1, 1));
            VertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColorTexture), Quad.Length, BufferUsage.None);
            VertexBuffer.SetData(Quad);

            ContentManager Content = SceneManagement.Instance.Content; //  TODO: PASS THIS IN 
            //File.WriteAllBytes("Content\\DeferredCombined.fx", Properties.Resources.DeferredCombined);
            //File.WriteAllBytes("Content\\MultiTarget.fx", Properties.Resources.MultiTarget);
            
            _lightEffect = Content.Load<Effect>("MultiTarget");
            CombinedEffect = Content.Load<Effect>("testCombine"); 

            _lightEffectTechniquePointLight = _lightEffect.Techniques["DeferredPointLight"];
            _lightEffectParameterLightColor = _lightEffect.Parameters["lightColor"];
            _lightEffectParameterLightDecay = _lightEffect.Parameters["lightDecay"];
            _lightEffectParameterNormapMap = _lightEffect.Parameters["NormalMap"];
            _lightEffectParameterPosition = _lightEffect.Parameters["lightPosition"];
            _lightEffectParameterScreenHeight = _lightEffect.Parameters["screenHeight"];
            _lightEffectParameterScreenWidth = _lightEffect.Parameters["screenWidth"];
            _lightEffectParameterStrength = _lightEffect.Parameters["lightStrength"];


            _lightEffectParameterConeDirection = _lightEffect.Parameters["coneDirection"];
            _lightEffectParameterConeAngle = _lightEffect.Parameters["coneAngle"];
            _lightEffectParameterConeDecay = _lightEffect.Parameters["coneDecay"];

            _lightEffectTechniqueSpotLight = _lightEffect.Techniques["DeferredSpotLight"];

        }

        public void SetScreenColor(Color color)
        {
            _graphicsColor = color; 
        }

        public void RegisterComponent(RenderComponent renderComponent)
        {

        }

        public void UnRegisterComponent(RenderComponent renderComponent)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GraphicsDevice.Clear(_graphicsColor);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, SceneManagement.Instance.CurrentScene.Camera.transform);
            

            for (int y = 0; y < SceneManagement.Instance.CurrentScene.CellSpacePartition.Width; y++)
            {
                for (int x = 0; x < SceneManagement.Instance.CurrentScene.CellSpacePartition.Height; x++)
                {
                    var entities = SceneManagement.Instance.CurrentScene.CellSpacePartition.RetrieveCell(x, y).Entities;
                    if(entities== null)
                        continue;
                    
                    foreach (var entity in entities)
                    {
                        if (entity.HasComponent<RenderComponent>())
                        {
                            var renderComponent = entity.GetComponent<RenderComponent>();

                            if (renderComponent.OverrideRender)
                            {
                                renderComponent.Draw(spriteBatch);
                                continue;
                            }

                            var texture =
                                TextureManager.Instance.GetTexture(renderComponent.TextureName);
                            var position = entity.GetComponent<TransformComponent>().Position;

                            if (!renderComponent.UsesCustomOrigin)
                            {
                                spriteBatch.Draw(texture, position,
                                    null, Color.White, MathHelper.ToRadians(renderComponent.Rotation),
                                    renderComponent.Origin, renderComponent.Scale,
                                    SpriteEffects.None, renderComponent.RenderLayer);
                            }
                        }
                    }
                }
            }

            if (SceneManagement.Instance.DrawDebug)
            {
                Debug.DrawDebug(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(spriteBatch);
        }

        public override void OnDestroy()
        {
            
        }

        public void DeferredDraw(SpriteBatch spriteBatch)
        {
            GraphicsDevice.Clear(Color.Black);

            // set render target
            GraphicsDevice.SetRenderTarget(DiffuseTarget);

            // clear render target
            GraphicsDevice.Clear(Color.Transparent);

            // draw diffuse
            DrawDiffuse(spriteBatch);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.SetRenderTarget(NormalTarget);

            //clear
            GraphicsDevice.Clear(Color.Transparent);

            // draw normal 
            DrawNormal(spriteBatch);

            GraphicsDevice.SetRenderTarget(null);

            GenerateShadowMap();
            
            GraphicsDevice.Clear(Color.Black);
            
            // draw combined
            DrawCombinedMaps(spriteBatch);

        }

        private void DrawCombinedMaps(SpriteBatch spriteBatch)
        {
            CombinedEffect.CurrentTechnique = CombinedEffect.Techniques["Combine"];
            CombinedEffect.Parameters["DiffuseTexture"].SetValue(DiffuseTarget);
            CombinedEffect.Parameters["ShadowTexture"].SetValue(ShadowTarget);
            CombinedEffect.Parameters["AmbientColor"].SetValue(AmbientColor.ToVector4());
            CombinedEffect.Parameters["AmbientStrength"].SetValue(AmbientStrength);
            CombinedEffect.Parameters["ShadowStrength"].SetValue(ShadowStrength);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, CombinedEffect);
            spriteBatch.Draw(DiffuseTarget, Vector2.Zero, Color.White);
            spriteBatch.End();
        }


        public void DrawDiffuse(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, SceneManagement.Instance.CurrentScene.Camera.transform);

            for (int y = 0; y < SceneManagement.Instance.CurrentScene.CellSpacePartition.Width; y++)
            {
                for (int x = 0; x < SceneManagement.Instance.CurrentScene.CellSpacePartition.Height; x++)
                {
                    var entities = SceneManagement.Instance.CurrentScene.CellSpacePartition.RetrieveCell(x, y).Entities;
                    if (entities == null)
                        continue;

                    foreach (var entity in entities)
                    {
                        var renderComponent = entity.GetComponent<RenderComponent>();
                        var material = renderComponent.Material;

                        var texture = material.GetDiffuse();
                        var position = entity.GetComponent<TransformComponent>().Position;

                        spriteBatch.Draw(texture, position,
                            null, Color.White, MathHelper.ToRadians(renderComponent.Rotation),
                            new Vector2(texture.Width / 2, texture.Height / 2), renderComponent.Scale,
                            SpriteEffects.None, renderComponent.RenderLayer);
                    }
                }
            }

            spriteBatch.End();
        }

        public void DrawNormal(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, SceneManagement.Instance.CurrentScene.Camera.transform);

            for (int y = 0; y < SceneManagement.Instance.CurrentScene.CellSpacePartition.Width; y++)
            {
                for (int x = 0; x < SceneManagement.Instance.CurrentScene.CellSpacePartition.Height; x++)
                {
                    var entities = SceneManagement.Instance.CurrentScene.CellSpacePartition.RetrieveCell(x, y).Entities;
                    if (entities == null)
                        continue;

                    foreach (var entity in entities)
                    {
                        var renderComponent = entity.GetComponent<RenderComponent>();
                        var material = renderComponent.Material;

                        if (material != null)
                        {
                            var texture = material.GetNormal();
                            var position = entity.GetComponent<TransformComponent>().Position;

                            spriteBatch.Draw(texture, position,
                                null, Color.White, MathHelper.ToRadians(renderComponent.Rotation),
                                new Vector2(texture.Width / 2, texture.Height / 2), renderComponent.Scale,
                                SpriteEffects.None, renderComponent.RenderLayer);
                        }
                    }
                }
            }

            spriteBatch.End();
        }

        private Texture2D GenerateShadowMap()
        {
            GraphicsDevice.SetRenderTarget(ShadowTarget);
            GraphicsDevice.Clear(Color.Transparent);

            foreach (var light in Lights)
            {
                if (!light.IsEnabled) continue;

                GraphicsDevice.SetVertexBuffer(VertexBuffer);

                // Draw all the light sources
                _lightEffectParameterStrength.SetValue((float)light.ActualPower);
                _lightEffectParameterPosition.SetValue(Vector3.Transform(light.Position, SceneManagement.Instance.CurrentScene.Camera.transform));
                _lightEffectParameterLightColor.SetValue(light.Color);
                _lightEffectParameterLightDecay.SetValue((float)light.LightDecay ); // Value between 0.00 and 2.00   
                _lightEffect.Parameters["specularStrength"].SetValue((float)SpecularStrength);

                if (light.LightType == LightType.Point)
                {
                    _lightEffect.CurrentTechnique = _lightEffectTechniquePointLight;
                }
                else
                {
                    _lightEffect.CurrentTechnique = _lightEffectTechniqueSpotLight;
                    _lightEffectParameterConeAngle.SetValue(((SpotLight)light).SpotAngle);
                    _lightEffectParameterConeDecay.SetValue(((SpotLight)light).SpotDecayExponent);
                    _lightEffectParameterConeDirection.SetValue(((SpotLight)light).Direction);
                }

                _lightEffectParameterScreenWidth.SetValue((float)GraphicsDevice.Viewport.Width);
                _lightEffectParameterScreenHeight.SetValue((float)GraphicsDevice.Viewport.Height);
                _lightEffect.Parameters["ambientColor"].SetValue(_ambientLight.ToVector4());
                _lightEffectParameterNormapMap.SetValue(NormalTarget);
                _lightEffect.Parameters["ColorMap"].SetValue(DiffuseTarget);
                _lightEffect.Parameters["Translation"].SetValue(Matrix.Invert(SceneManagement.Instance.CurrentScene.Camera.transform));
                _lightEffect.CurrentTechnique.Passes[0].Apply();

                // Add Belding (Black background)
                GraphicsDevice.BlendState = BlendBlack;

                // Draw some magic

                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, Quad, 0, 2);
            }

            // Deactive the rander targets to resolve them
            GraphicsDevice.SetRenderTarget(null);

            return ShadowTarget;
        }

        public void DrawDebugRenderTargets(SpriteBatch spriteBatch)
        {
            // Draw some debug textures
            spriteBatch.Begin();

            Rectangle size = new Rectangle(0, 0, DiffuseTarget.Width / 4, DiffuseTarget.Height / 4);
            var position = new Vector2(0, GraphicsDevice.Viewport.Height - size.Height);
            spriteBatch.Draw(
                DiffuseTarget,
                new Rectangle(
                    (int)0, (int)position.Y,
                    size.Width,
                    size.Height),
                Color.White);

            spriteBatch.Draw(
                NormalTarget,
                new Rectangle(
                    (int)size.Width, (int)position.Y,
                    size.Width,
                    size.Height),
                Color.White);

            spriteBatch.Draw(
                ShadowTarget,
                new Rectangle(
                    (int)size.Width * 2, (int)position.Y,
                    size.Width,
                    size.Height),
                Color.White);

            spriteBatch.End();
        }

        public static BlendState BlendBlack = new BlendState()
        {
            ColorBlendFunction = BlendFunction.Add,
            ColorSourceBlend = Blend.One,
            ColorDestinationBlend = Blend.One,

            AlphaBlendFunction = BlendFunction.Add,
            AlphaSourceBlend = Blend.SourceAlpha,
            AlphaDestinationBlend = Blend.One
        };

    }


}
