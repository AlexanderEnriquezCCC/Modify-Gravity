using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Modify_Gravity_andor_Jump
{
    /// <summary>
    /// Simple Movement For Jumping
    /// Uses a simple class called KeyboardHandler for input
    /// make a player class that has movement method, each Keys key is linked to IsKeyPressedDir in KeyboardHandler
    /// Player carries values for jump height, accel, and max speed, location 
    /// </summary>
    class Player
    {
        KeyboardHandler keyboardHandler;

        public float playerAccel;
        public float playerJumpHeight;
        public float playerMaxSpeed;
        public bool playerOnGround;
        public Texture2D playerImg;
        public Vector2 playerLoc, playerDir;
        
        public Player(float playerAccel,float playerJumpHeight,float playerMaxSpeed,Vector2 playerLoc, Vector2 playerDir, Texture2D playerImg, bool playerOnGround)
        {
            this.playerAccel = playerAccel;
            this.playerJumpHeight = playerJumpHeight;
            this.playerLoc = playerLoc;
            this.playerDir = playerDir;
            this.playerMaxSpeed = playerMaxSpeed;
            this.playerOnGround = playerOnGround;
            this.playerImg = playerImg;
            keyboardHandler = new KeyboardHandler();
        }


        public void playerMovement(Keys jump, Keys left, Keys right, float friction)
        {
            keyboardHandler.Update();
            //jump
            if (keyboardHandler.WasKeyPressed(jump))
            {
                this.playerDir = this.playerDir + new Vector2(0, this.playerJumpHeight);
            }

            if (this.playerOnGround)
            {
                //left/right movement on ground not in air
                if((!(keyboardHandler.IsHoldingKey(left))) && (!(keyboardHandler.IsHoldingKey(right))))
                {
                    if (this.playerDir.X > 0)
                    {
                        this.playerDir.X = Math.Max(0, this.playerDir.X - friction);
                    }
                    else 
                    {
                        this.playerDir.X = Math.Max(0, this.playerDir.X + friction);
                    }
                }

                if (keyboardHandler.IsHoldingKey(left))
                {
                    this.playerDir.X = Math.Max((this.playerMaxSpeed * -1.0f), this.playerDir.X - this.playerAccel);
                }
                if (keyboardHandler.IsHoldingKey(right))
                {
                    this.playerDir.X = Math.Max(this.playerMaxSpeed, this.playerDir.X + this.playerAccel);
                }
            }
        }
    }
}
