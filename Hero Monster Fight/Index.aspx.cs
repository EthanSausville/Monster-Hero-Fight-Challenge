using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hero_Monster_Fight
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Setting hero stats
            Character hero = new Character();
            hero.name = "Knight";
            hero.health = 50;
            hero.damageMax = 20;
            hero.bonusAttack = true;

            // Setting monster stats
            Character monster = new Character();
            monster.name = "Ogre";
            monster.health = 100;
            monster.damageMax = 12;
            monster.bonusAttack = false;

            if (hero.bonusAttack) monster.health = hero.Attack(monster.health); // Hero Bonus Attack
            if (monster.bonusAttack) hero.health = monster.Attack(hero.health); // Monster Bonus Attack
            if (hero.bonusAttack || monster.bonusAttack) printRound(true,hero,monster); // Print results

            // Loop for the remainder of the Battle
            while (hero.health > 0 && monster.health > 0)
            {
                monster.health = hero.Attack(monster.health); // Hero Attack
                hero.health = monster.Attack(hero.health); // Monster Attack
                printRound(false,hero,monster);
            }

            // Print Results of battle
            if (hero.health <= 0 && monster.health <= 0) resultLabel.Text += "The hero killed the monster, but died trying";
            else if (hero.health <= 0) resultLabel.Text += "The hero died";
            else resultLabel.Text += "The hero was victorious.";
         }

        // Method to print results of each round of combat
        private void printRound(bool bonusRound, Character hero, Character monster)
        {
            if (bonusRound)
                resultLabel.Text += string.Format("<p>BONUS ROUND: Hero Health is: {0}, and Monster Health is: {1}</p>", hero.health, monster.health);
            else
                resultLabel.Text += string.Format("<p>Hero Health is: {0}, and Monster Health is: {1}</p>", hero.health, monster.health);
        }

        // Character class
        class Character
        {
            Random randomNum = new Random();

            public string name { get; set; }
            public int health { get; set; }
            public int damageMax { get; set; }
            public bool bonusAttack { get; set; }

            public int Attack(int enemyHealth)
            {
                enemyHealth -= randomNum.Next(1, this.damageMax);
                return enemyHealth;
            }
        }
    }
}