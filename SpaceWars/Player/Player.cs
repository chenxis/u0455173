


/// <summary>
/// 
/// @authur Chenxi Sun and Khang Lieu
/// </summary>
namespace SpaceWars
{
    public class Player
    {
        int ID; 
        string name; 
        int shotsFired; 
        int hits; 
        int score; 
        int FramesSinceDead;

        int FramesSinceLastShot; 

        int FramesPerShot; 

        bool recentThrust; 
        bool recentLeft;
        bool recentRight; 
        bool recentProjectile; 

        bool recentScore; 
        



        public Player(string playerName, int playerID, int frames)
        {
            name = playerName;
            ID = playerID;
            shotsFired = 0;
            hits = 0;
            score = 0;
            FramesSinceLastShot = 0;
            FramesSinceDead = 0;
            FramesPerShot = frames;
            

             recentThrust =false;
             recentLeft = false;
             recentRight = false;
             recentProjectile = false;
             recentScore = false;
        }
        
        
        /// <summary>
        /// Called every time a player scores 
        /// </summary>
        public void AddHit()
        {
            hits+=1;
        }

        /// <summary>
        /// Gets the number of hits
        /// </summary>
        /// <returns>returns the number of hits</returns>
        public int GetHits()
        {
            return hits;
        }

        /// <summary>
        /// increments the player's score
        /// </summary>
        public void AddScore()
        {
            score+=1;
        }

        /// <summary>
        /// Gets the player's score
        /// </summary>
        /// <returns>player's score</returns>
        public int GetScore()
        {
            return score;
        }

        public void AddShotsFired()
        {
            shotsFired++;
        }
        public int GetShotsFired()
        {
            return shotsFired;
        }

        public int GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return name;
        }

        public void SetRecentRight(bool input)
        {
            recentRight = input;
        }

        public bool GetRecentRight()
        {
            return recentRight;
        }
        public void SetRecentLeft(bool input)
        {
            recentLeft = input;
        }

        public bool GetRecentLeft()
        {
            return recentLeft;
        }
        public int GetProjectilesOnScreen()
        {
            return FramesSinceLastShot;
        }
        public void RemoveProjectilesOnScreen()
        {
            FramesSinceLastShot--;
        }


        public void SetRecentThrust(bool input)
        {
             recentThrust = input;
        }

        public bool GetRecentThrust()
        {
            return recentThrust;
        }
        public bool GetRecentProjectile()
        {
            return recentProjectile;
        }

     
        public bool ReadyToFire()
        {
            if (FramesSinceLastShot >= FramesPerShot)
            {
                return true;

            }
            else
                return false;
        }

        public void ProjectileFired()
        {
            FramesSinceLastShot = 0;
        }

        public void AddProjectileFrame()
        {
            if (FramesSinceLastShot<FramesPerShot)
            {
                FramesSinceLastShot++;

            }
        }

        public void SetRecentProjectile(bool input)
        {
            recentProjectile = input;
        }

        public int GetFramesSinceDead()
        {
            return FramesSinceDead;
        }

        /// <summary>
        /// Increments the framesSinceDead by 1
        /// </summary>
        public void IncrementFramesSinceDead()
        {
            FramesSinceDead++;
        }

        public void ResetFramesSinceDead()
        {
            FramesSinceDead = 0;
        }
        public void SetRecentScore(bool input)
        {
            recentScore = input;
        }
        public bool GetRecentScore()
        {
            return recentScore;
        }

    }
}
