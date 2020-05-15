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
        public RenderTarget2D LightsTarget;
        public RenderTarget2D ShadowTarget;

        public VertexPositionColorTexture[] Quad;
        public VertexBuffer VertexBuffer;
        private Effect _lightEffect;
        private Effect _lightCombinedEffect;

        public List<Light> Lights = new List<Light>();
        private Color _ambientLight = new Color(.1f, .1f, .1f, 1);
        private float _specularStrength = 1.0f;

        private EffectTechnique _lightEffectTechniquePointLight;
        private EffectParameter _lightEffectParameterStrength;
        private EffectParameter _lightEffectParameterPosition;
        private EffectParameter _lightEffectParameterConeDirection;
        private EffectParameter _lightEffectParameterLightColor;
        private EffectParameter _lightEffectParameterLightDecay;
        private EffectParameter _lightEffectParameterScreenWidth;
        private EffectParameter _lightEffectParameterScreenHeight;
        private EffectParameter _lightEffectParameterNormapMap;

        private EffectTechnique _lightCombinedEffectTechnique;
        private EffectParameter _lightCombinedEffectParamAmbient;
        private EffectParameter _lightCombinedEffectParamLightAmbient;
        private EffectParameter _lightCombinedEffectParamAmbientColor;
        private EffectParameter _lightCombinedEffectParamColorMap;
        private EffectParameter _lightCombinedEffectParamShadowMap;
        private EffectParameter _lightCombinedEffectParamNormalMap;

        public GraphicsDevice GraphicsDevice;
        public RenderManager()
        {
            GraphicsDevice = SceneManagement.Instance.CurrentScene.GraphicsDevice;

            var width = GraphicsDevice.PresentationParameters.BackBufferWidth;
            var height = GraphicsDevice.PresentationParameters.BackBufferHeight;
            DiffuseTarget = new RenderTarget2D(GraphicsDevice, width, height);
            NormalTarget = new RenderTarget2D(GraphicsDevice, width, height);
            LightsTarget = new RenderTarget2D(GraphicsDevice, width, height);

            Quad = new VertexPositionColorTexture[4];
            Quad[0] = new VertexPositionColorTexture(new Vector3(-1, 1, 0), Color.White, new Vector2(0, 0));
            Quad[1] = new VertexPositionColorTexture(new Vector3(1, 1, 0), Color.White, new Vector2(1, 0));
            Quad[2] = new VertexPositionColorTexture(new Vector3(-1, -1, 0), Color.White, new Vector2(0, 1));
            Quad[3] = new VertexPositionColorTexture(new Vector3(1, -1, 0), Color.White, new Vector2(1, 1));
            VertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColorTexture), Quad.Length, BufferUsage.None);
            VertexBuffer.SetData(Quad);

            if (SceneManagement.Instance.CurrentScene.DeferredDraw)
            {
                ContentManager Content = SceneManagement.Instance.Content; //  TODO: PASS THIS IN 
                File.WriteAllBytes("Content\\DeferredCombined.fx", Properties.Resources.DeferredCombined);
                File.WriteAllBytes("Content\\MultiTarget.fx", Properties.Resources.MultiTarget);


                _lightCombinedEffect = Content.Load<Effect>("DeferredCombined");

                _lightEffect = Content.Load<Effect>("MultiTarget");

                _lightEffectTechniquePointLight = _lightEffect.Techniques["DeferredPointLight"];
                _lightEffectParameterConeDirection = _lightEffect.Parameters["coneDirection"];
                _lightEffectParameterLightColor = _lightEffect.Parameters["lightColor"];
                _lightEffectParameterLightDecay = _lightEffect.Parameters["lightDecay"];
                _lightEffectParameterNormapMap = _lightEffect.Parameters["NormalMap"];
                _lightEffectParameterPosition = _lightEffect.Parameters["lightPosition"];
                _lightEffectParameterScreenHeight = _lightEffect.Parameters["screenHeight"];
                _lightEffectParameterScreenWidth = _lightEffect.Parameters["screenWidth"];
                _lightEffectParameterStrength = _lightEffect.Parameters["lightStrength"];

                _lightCombinedEffectTechnique = _lightCombinedEffect.Techniques["DeferredCombined2"];
                _lightCombinedEffectParamAmbient = _lightCombinedEffect.Parameters["ambient"];
                _lightCombinedEffectParamLightAmbient = _lightCombinedEffect.Parameters["lightAmbient"];
                _lightCombinedEffectParamAmbientColor = _lightCombinedEffect.Parameters["ambientColor"];
                _lightCombinedEffectParamColorMap = _lightCombinedEffect.Parameters["ColorMap"];
                _lightCombinedEffectParamShadowMap = _lightCombinedEffect.Parameters["ShadingMap"];
                _lightCombinedEffectParamNormalMap = _lightCombinedEffect.Parameters["NormalMap"];
            }

        }
        
        public void RegisterComponent(RenderComponent renderComponent)
        {

        }

        public void UnRegisterComponent(RenderComponent renderComponent)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
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
                        var renderComponent = entity.GetComponent<RenderComponent>();
                        var material = renderComponent.Material;

                        if (material == null)
                        {
                            var texture =
                                TextureManager.Instance.GetTexture(renderComponent.TextureName);
                            var position = entity.GetComponent<TransformComponent>().Position;

                            spriteBatch.Draw(texture, position,
                                null, Color.White, MathHelper.ToRadians(renderComponent.Rotation),
                                new Vector2(texture.Width / 2, texture.Height / 2), renderComponent.Scale,
                                SpriteEffects.None, renderComponent.RenderLayer);
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
            _lightCombinedEffect.CurrentTechnique = _lightCombinedEffectTechnique;
            _lightCombinedEffectParamAmbient.SetValue(1f);
            _lightCombinedEffectParamLightAmbient.SetValue(4f);
            _lightCombinedEffectParamAmbientColor.SetValue(_ambientLight.ToVector4());
            _lightCombinedEffectParamColorMap.SetValue(DiffuseTarget);
            _lightCombinedEffectParamShadowMap.SetValue(ShadowTarget);
            _lightCombinedEffectParamNormalMap.SetValue(NormalTarget);
            _lightCombinedEffect.CurrentTechnique.Passes[0].Apply();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, _lightCombinedEffect);
            spriteBatch.Draw(DiffuseTarget, new Rectangle(0,0, 100,100), Color.White);
            spriteBatch.End();
        }


        public void DrawDiffuse(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, SceneManagement.Instance.CurrentScene.Camera.transform);

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
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, SceneManagement.Instance.CurrentScene.Camera.transform);

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

                        var texture = material.GetNormal();
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

        private Texture2D GenerateShadowMap()
        {
            GraphicsDevice.SetRenderTarget(ShadowTarget);
            GraphicsDevice.Clear(Color.Transparent);

            foreach (var light in Lights)
            {
                if (!light.IsEnabled) continue;

                GraphicsDevice.SetVertexBuffer(VertexBuffer);

                // Draw all the light sources
                _lightEffectParameterStrength.SetValue(light.ActualPower);
                _lightEffectParameterPosition.SetValue(light.Position);
                _lightEffectParameterLightColor.SetValue(light.Color);
                _lightEffectParameterLightDecay.SetValue((float)light.LightDecay); // Value between 0.00 and 2.00   
                _lightEffect.Parameters["specularStrength"].SetValue((float)_specularStrength);

                if (light.LightType == LightType.Point)
                {
                    _lightEffect.CurrentTechnique = _lightEffectTechniquePointLight;
                }

                _lightEffectParameterScreenWidth.SetValue((float)GraphicsDevice.Viewport.Width);
                _lightEffectParameterScreenHeight.SetValue((float)GraphicsDevice.Viewport.Height);
                _lightEffect.Parameters["ambientColor"].SetValue(_ambientLight.ToVector4());
                _lightEffectParameterNormapMap.SetValue(NormalTarget);
                _lightEffect.Parameters["ColorMap"].SetValue(DiffuseTarget);
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
