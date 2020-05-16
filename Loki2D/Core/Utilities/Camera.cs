using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loki2D.Core.Component;
using Loki2D.Core.GameObject;
using Loki2D.Core.Scene;
using Loki2D.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Loki2D.Core.Utilities
{
    public class Camera
    {
        public static Camera Instance;

        public Matrix transform;

        Viewport viewPort;
        public Vector2 center;
        public Vector2 lerpedCenter;
        public float ZoomScale = 1f;
        private float _minScale = .0001f;
        private float _maxScale = 4;

        public bool CamControl = true;
        public float Speed = 60;
        private float originalSpeed = 60;
        private float _zoomSpeed = .05f;
        public static Vector2 position;
        private Entity focusedEntity;

        private int _topLeft;
        private int _topRight;
        private int _bottomLeft;
        private int _bottomRight;

        public bool Scrolling { get; private set; }

        public Camera(Viewport vPort)
        {
            Instance = this;
            position = new Vector2(0, 0);
            viewPort = vPort;
            Scrolling = true;
        }

        [System.Obsolete("Use CanUpdate() instead", true)]
        public void Update(GameTime gameTime)
        {

        }

        public void Update()
        {
            
            UpdateCameraBoundaries();

            if (Scrolling)
            {
                if (InputManager.MouseState.ScrollWheelValue > InputManager.OldMouseState.ScrollWheelValue)
                {
                    if (!CamControl)
                    {
                        ZoomScale += .025f;
                    }
                    else
                    {
                        ZoomScale += _zoomSpeed;
                    }
                }

                if (InputManager.MouseState.ScrollWheelValue < InputManager.OldMouseState.ScrollWheelValue)
                {
                    if (!CamControl)
                    {
                        ZoomScale -= .025f;
                    }
                    else
                    {
                        ZoomScale -= _zoomSpeed;
                    }
                }
            }
            // Tree tree = new Tree(Vector2.Zero, 0, 0);

            if (ZoomScale > _maxScale)
            {
                ZoomScale = _maxScale;
            }
            if (ZoomScale <= _minScale)
            {
                ZoomScale = _minScale;
            }




            if (InputManager.KeyPressed(Keys.F1) && focusedEntity != null)
            {
                CamControl = !CamControl;
            }

            if (CamControl)
            {
                center = position;
                lerpedCenter = Vector2.Lerp(lerpedCenter, center, .5f);

                if (InputManager.KeyDown(Keys.Up)) /*|| keyboardState.IsKeyDown(Keys.W)*/
                {
                    position.Y -= Speed;
                }
                if (InputManager.KeyDown(Keys.Down) /*|| keyboardState.IsKeyDown(Keys.S)*/)
                {
                    position.Y += Speed;
                }
                if (InputManager.KeyDown(Keys.Right) /*|| keyboardState.IsKeyDown(Keys.D)*/)
                {
                    position.X += Speed;
                }
                if (InputManager.KeyDown(Keys.Left) /*|| keyboardState.IsKeyDown(Keys.A)*/)
                {
                    position.X -= Speed;
                }
                if (InputManager.KeyDown(Keys.LeftShift))
                {
                    Speed = (originalSpeed * 3);
                }
                else
                {
                    Speed = originalSpeed;
                }

            }
            else
            {
                if (focusedEntity == null)
                {
                    //center = Game1.player.GetCenter();
                    center = position;
                }
                else
                {
                    center = focusedEntity.GetComponent<TransformComponent>().Position;
                    position = center;
                    lerpedCenter = Vector2.Lerp(lerpedCenter, center, .1f);

                }

            }


            transform = Matrix.CreateTranslation(new Vector3(-lerpedCenter.X, -lerpedCenter.Y, 0)) *
                Matrix.CreateScale(new Vector3(ZoomScale, ZoomScale, 1)) *
                Matrix.CreateTranslation(new Vector3(viewPort.Width * 0.5f, viewPort.Height * 0.5f, 0));

        }

        public void WSADMovement()
        {
            if (InputManager.KeyDown(Keys.W) /*|| keyboardState.IsKeyDown(Keys.W)*/)
            {
                position.Y -= Speed;
            }
            if (InputManager.KeyDown(Keys.S) /*|| keyboardState.IsKeyDown(Keys.S)*/)
            {
                position.Y += Speed;
            }
            if (InputManager.KeyDown(Keys.D) /*|| keyboardState.IsKeyDown(Keys.D)*/)
            {
                position.X += Speed;
            }
            if (InputManager.KeyDown(Keys.A) /*|| keyboardState.IsKeyDown(Keys.A)*/)
            {
                position.X -= Speed;
            }
        }

        public Vector2 TopLeftPosition { get; private set; }
        public Vector2 TopRightPosition { get; private set; }
        public Vector2 BottomLeftPosition { get; private set; }
        public Vector2 BottomRightPosition { get; private set; }
        private void UpdateCameraBoundaries()
        {
            TopLeftPosition = Vector2.Transform(new Vector2(0, 0), Matrix.Invert(transform));
            TopRightPosition =
               Vector2.Transform(new Vector2(viewPort.Width, 0), Matrix.Invert(transform));
            BottomLeftPosition = Vector2.Transform(new Vector2(0, viewPort.Height), Matrix.Invert(transform));
            BottomRightPosition =
               Vector2.Transform(new Vector2(viewPort.Width, viewPort.Height), Matrix.Invert(transform));

            //_topLeft = CellSpacePartition.GetTopLeftPartition(TopLeftPosition);
            //_topRight = CellSpacePartition.GetTopRightPartition(TopRightPosition);
            //_bottomLeft = CellSpacePartition.GetBottomLeftPartition(BottomLeftPosition);
            //_bottomRight = CellSpacePartition.GetBottomRightPartition(BottomRightPosition);


        }

        public int GetTopLeftCell()
        {
            return _topLeft;
        }

        public int GetTopRightCell()
        {
            return _topRight;
        }

        public int GetBottomLeft()
        {
            return _bottomLeft;
        }

        public int GetBottomRight()
        {
            return _bottomRight;
        }

        /// <summary>
        /// How far the camera zooms in
        /// larger number the more youre able to zoom in
        /// </summary>
        /// <param name="zoom"></param>
        public void SetMaxZoom(float zoom = 4)
        {
            _maxScale = zoom;
        }

        /// <summary>
        /// How far the camera zooms out
        /// smaller number the farther it zooms out
        /// </summary>
        /// <param name="zoom"></param>
        public void SetMinZoom(float zoom = .0001f)
        {
            _minScale = zoom;
        }

        public void SetZoom(float zoom = 1)
        {
            ZoomScale = zoom;
        }

        public void SetZoomSpeed(float zoomSpeed = .0005f)
        {
            _zoomSpeed = zoomSpeed;
        }

        public void SetFocus(Entity entity)
        {
            focusedEntity = entity;
        }

        public void SetMoveSpeed(float speed = 60)
        {
            this.Speed = speed;
            originalSpeed = speed;
        }

        /// <summary>
        /// enable and disables scroll
        /// </summary>
        /// <param name="canScroll"></param>
        public void SetScroll(bool canScroll)
        {
            Scrolling = canScroll;
        }

        public Entity GetFocus()
        {
            return focusedEntity;
        }


        public Vector2 WindowToCameraSpace(Vector2 windowPosition)
        {
            // Scale for camera bounds that vary from window
            // Also, must adjust for translation if camera isn't at 0, 0 in screen space (such as a mini-map)
            return (ZoomScale * windowPosition) + center;
        }

    }
}
