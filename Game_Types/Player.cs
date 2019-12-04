using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GameTypes
{
    [Serializable]
    public class Player : GameItem
    {
        Vec2 velocity = new Vec2(0, 0);
        Rect frame = new Rect(0, 0,35,105);
        Col color = new Col(255,255,255);
        Col dmgColor = new Col(255,0,0);
        float gravity = 0.6f;
        bool colliding = false;
        bool collidingTop = false;
        bool collidingBottom = false;
        bool collidingLeft = false;
        bool collidingRight = false;
        int normHeight;
        bool ducking = false;
        Weapon equippedWeapon;
        int animationCount = 0;
        AnimFrame frameID = AnimFrame.STAND;
        int hp = 100;
        bool isDead = false;
        int damageCounter = 0;

        Keys[] inputs = new Keys[4];
        bool attackInputLeft = true;

        enum AnimFrame { STAND,LEFT,RIGHT,JUMP,DUCK,PUNCH_RIGHT, PUNCH_LEFT }
        enum KeyBinds { JUMP,LEFT,RIGHT,DUCK }
        enum MouseBinds { ATTACK }

        public Player(int x, int y, int width, int height, Color color, Keys[] inputs, bool attackInput) : base(x, y, width, height)
        {
            normHeight = height;
            equippedWeapon = new Unarmed(x,y);
            this.color = new Col(color);
            this.dmgColor = new Col(Color.Lerp(Color.Red, color, 0.5f));
           
            this.inputs = inputs;
            this.attackInputLeft = attackInput;
        }

        public Vec2 GetVelocity() { return this.velocity; }
        public bool IsDead() { return this.isDead; }

        public void WeaponCollide(List<Platform> platforms, List<Player> players) 
        {
            equippedWeapon.UpdateOrigin(this.hitbox.X, this.hitbox.Y);
            if (!this.isDead)
            {
                MouseState newMouseState = Mouse.GetState();
                bool attack = false;
                if ((attackInputLeft ? newMouseState.LeftButton : newMouseState.RightButton) == ButtonState.Pressed) //&& (attackInputLeft ? oldMouseState.LeftButton : oldMouseState.RightButton) == ButtonState.Released)
                {
                    attack = true;
                    SetFrame(newMouseState.X < this.hitbox.X ? AnimFrame.PUNCH_LEFT : AnimFrame.PUNCH_RIGHT);
                }
                equippedWeapon.SetCollide(false);
                foreach (Player player in players)
                {
                    if (player != this)
                    {
                        if (!player.IsDead())
                        {
                            if (equippedWeapon.Collide(player.GetHitbox()))
                            {
                                if (attack)
                                {
                                    equippedWeapon.Attack(player);
                                    equippedWeapon.SetCollide(false);
                                }
                            }
                        }
                    }
                }
                //oldMouseState = newMouseState;
            }
        }
        public void SetVelocity(Vec2 newVelocity) { this.velocity = newVelocity; }
        public void SetVelocity(float x, float y) { this.velocity.X = x; this.velocity.Y = y; }
        public virtual void AdjustVelocity(float x, float y)
        {
            this.velocity.X += x;
            this.velocity.Y += y;
        }

        public void Damage(int dmg) { damageCounter = 500;  hp -= dmg; if (hp <= 0) Kill(); }
        public void CheckBorders() 
        {
            bool left = hitbox.X < -hitbox.Width;
            bool right = hitbox.X > 1440 + hitbox.Width;
            bool up = hitbox.Y < -hitbox.Height;
            bool down = hitbox.Y < 810 + hitbox.Height;
            if(left || right || up || down)
                Kill();
        }

        protected void Kill()
        {
            isDead = true;
        }

        protected override Rectangle GetDrawHitbox() { return new Rectangle(this.hitbox.X - (int)(Math.Abs(this.frame.Width)*0.5f), this.hitbox.Y - (int)(this.frame.Height * 0.5f), Math.Abs(this.frame.Width), this.frame.Height); }

        public void SetCollidingTop(bool colliding)     { this.collidingTop = colliding;    this.colliding = colliding; }
        public void SetCollidingBottom(bool colliding)  { this.collidingBottom = colliding; this.colliding = colliding;}
        public void SetCollidingLeft(bool colliding)    { this.collidingLeft = colliding;   this.colliding = colliding;}
        public void SetCollidingRight(bool colliding)   { this.collidingRight = colliding;  this.colliding = colliding;}
        public void SetColliding(bool colliding)        { this.colliding = colliding; }
        public bool IsColliding() { return this.colliding; }
        public void SetDuck(bool ducking)
        {
            this.hitbox.Height = ducking ? (int)(this.normHeight * 0.5f) : this.normHeight;
            this.hitbox.Y = this.hitbox.Y + (ducking ? (int)(this.normHeight * 0.25f) : (int)(this.normHeight * -0.25f));
            this.ducking = ducking;
        }

        void SetFrame(AnimFrame frameID)
        {
            switch(frameID)
            {
                case AnimFrame.DUCK:
                    frame = new Rect(70, 0, 35, 50);
                    animationCount = 0;
                    break;
                case AnimFrame.STAND:
                    frame = new Rect(0, 0,35,100);
                    animationCount = 0;
                    break;
                case AnimFrame.LEFT:
                    if (animationCount <= 0)
                    {
                        animationCount = 150;
                        if (this.frameID == AnimFrame.LEFT && frame.X == 175)
                            frame = new Rect(245, 0, -70, 100);
                        else
                            frame = new Rect(175, 0, -70, 100);
                    }
                    break;
                case AnimFrame.RIGHT:
                    animationCount = 150;
                    if (this.frameID == AnimFrame.RIGHT && frame.X == 105)
                        frame = new Rect(175, 0, 70, 100);
                    else
                        frame = new Rect(105, 0, 70, 100);
                    break;
                case AnimFrame.JUMP:
                    frame = new Rect(35, 0, 35, 100);
                    animationCount = 0;
                    break;
                case AnimFrame.PUNCH_LEFT:
                    frame = new Rect(315, 0, -70, 100);
                    animationCount = 200;
                    break;
                case AnimFrame.PUNCH_RIGHT:
                    frame = new Rect(245, 0, 70, 100);
                    animationCount = 200;
                    break;
            }
            this.frameID = frameID;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isDead)
            {
                if (this.texture.GetTexture(spriteBatch.GraphicsDevice) != null)
                    spriteBatch.Draw(this.texture.GetTexture(spriteBatch.GraphicsDevice), GetDrawHitbox(), frame.Convert(), damageCounter > 0 ? this.dmgColor.Convert() : this.color.Convert());
                equippedWeapon.Draw(spriteBatch);
            }
        }
        public void Update(GameTime gameTime)
        {
            if (!isDead)
            {
                KeyboardState newKeyboardState = Keyboard.GetState();

                if (newKeyboardState.IsKeyDown(inputs[(int)KeyBinds.JUMP]) && colliding)//&& oldKeyboardState.IsKeyUp(inputs[(int)KeyBinds.JUMP]))
                    this.SetVelocity(this.velocity.X, -3.0f);
                if (newKeyboardState.IsKeyDown(inputs[(int)KeyBinds.LEFT]) && !collidingLeft)
                    this.AdjustVelocity(-0.1f, 0);
                if (newKeyboardState.IsKeyDown(inputs[(int)KeyBinds.RIGHT]) && !collidingRight)
                    this.AdjustVelocity(0.1f, 0);
                if (newKeyboardState.IsKeyDown(inputs[(int)KeyBinds.DUCK]))// && oldKeyboardState.IsKeyUp(inputs[(int)KeyBinds.DUCK]))
                    this.SetDuck(true);
                if (newKeyboardState.IsKeyUp(inputs[(int)KeyBinds.DUCK]))// && oldKeyboardState.IsKeyDown(inputs[(int)KeyBinds.DUCK]))
                    this.SetDuck(false);


                //oldKeyboardState = newKeyboardState;

                //Initial Update
                colliding = false;
                collidingTop = false;
                collidingBottom = false;
                collidingLeft = false;
                collidingRight = false;
                if (damageCounter > 0)
                    damageCounter -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                float dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                dt = dt > 50f ? 50f : dt;
                SetHitbox((int)(this.hitbox.X + (this.velocity.X * dt)), (int)(this.hitbox.Y + ((this.velocity.Y + gravity) * dt)), this.hitbox.Width, this.hitbox.Height);

                // Account for air resistance / friction
                this.velocity.X = Math.Abs(this.velocity.X) * 0.9f * (velocity.X < 0 ? -1 : 1);
                this.velocity.X = Math.Abs(this.velocity.X) < 0.01f ? 0 : this.velocity.X;
                this.velocity.Y *= 0.9f;
                this.velocity.Y = Math.Abs(this.velocity.Y) < 0.01f ? 0 : this.velocity.Y;

                animationCount -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (animationCount <= 0)
                {
                    if (velocity.Y == 0)
                    {
                        if (!ducking)
                        {
                            if (velocity.X > 0.1)
                                SetFrame(AnimFrame.RIGHT);
                            else if (velocity.X < -0.1)
                                SetFrame(AnimFrame.LEFT);
                            else
                                SetFrame(AnimFrame.STAND);
                        }
                        else
                            SetFrame(AnimFrame.DUCK);
                    }
                    else
                        SetFrame(AnimFrame.JUMP);
                }
            }
        }
    }
}
