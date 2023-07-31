namespace FootballManager.Infrastructure.Data.Constant
{
    public class MatchMessages
    {
        public class Messages
        {
            public readonly List<(string message, bool retainsPossession)> DribblingMessages = new List<(string, bool)>
            {
                ("{0} showcases incredible dribbling skills, leaving defenders in their wake.", true),
                ("{0} weaves through a maze of defenders with precise ball control.", true),
                ("{0} uses quick footwork to shimmy and shake past opponents.", true),
                ("{0} pulls off a cheeky nutmeg, gaining the admiration of the crowd.", true),
                ("{0} glides past defenders with ease, displaying their dribbling wizardry.", true),
                ("{0} dances through the defense, leaving them mesmerized.", true),
                ("{0} displays impeccable ball control while dribbling at high speed.", true),
                ("{0} effortlessly takes on multiple opponents, making it look easy.", true),
                ("{0} uses a series of feints to create space and advance with the ball.", true),
                ("{0} executes a perfectly timed skill move to beat the defender.", true),
                ("{0} loses control of the ball under pressure from the opposition.", false),
                ("{0} attempts a skillful dribble but gets dispossessed by a well-timed tackle.", false),
                ("{0} shows off some fancy footwork but loses balance and falls.", false),
                ("{0} tries to dribble through defenders but gets crowded out.", false),
                ("{0} attempts to beat a defender with a stepover but fails.", false),
                ("{0} is tackled and loses the ball while trying to take on multiple opponents.", false),
            };

            public readonly List<(string message, bool retainsPossession)> TacklingMessages = new List<(string, bool)>
            {
                ("{0} executes a crunching tackle, winning the ball back with authority.", true),
                ("{0} makes a perfectly timed sliding tackle to dispossess the opponent.", true),
                ("{0} showcases excellent defensive skills with a standing tackle.", true),
                ("{0} commits a tactical foul to stop a dangerous counter-attack.", false),
                ("{0} intercepts a pass with their smart positioning and anticipation.", true),
                ("{0} displays impressive recovery speed, catching up to the attacker and making a clean tackle.", true),
                ("{0} puts in a last-ditch block to deny a shot on goal.", true),
                ("{0} dispossesses the opponent with a well-timed poke tackle.", true),
                ("{0} shows great physicality in winning the ball with a shoulder challenge.", true),
                ("{0} makes a sliding challenge, cleanly winning the ball without fouling.", true),
                ("{0} attempts a tackle but mistimes it and gives away a foul.", false),
                ("{0} goes in for a tackle but gets nutmegged by the opponent.", false),
                ("{0} attempts a sliding tackle but misses, allowing the opponent to advance.", false),
                ("{0} is booked after making a reckless challenge from behind.", false),
                ("{0} gets caught out of position and fails to intercept a pass.", false),
                ("{0} tries to tackle but loses balance and falls to the ground.", false),
            };

            public readonly List<(string message, bool retainsPossession)> ShootingMessages = new List<(string, bool)>
            {
                ("{0} unleashes a powerful shot from distance, narrowly missing the target.", false),
                ("{0} attempts a finesse shot, aiming for the far post but just wide.", false),
                ("{0} smashes the ball into the top corner with a thunderous strike.", true),
                ("{0} sends a dipping shot from outside the box, testing the goalkeeper's reflexes.", false),
                ("{0} strikes the ball with precision, but the goalkeeper makes a great save.", false),
                ("{0} volleys the ball with technique, forcing the keeper into a fingertip save.", false),
                ("{0} places the shot into the bottom corner, leaving the goalkeeper rooted to the spot.", true),
                ("{0} tries a chip shot, but it goes just over the crossbar.", false),
                ("{0} goes for a powerful long-range effort, narrowly missing the target.", false),
                ("{0} attempts a curling shot, aiming for the top corner, but it goes wide.", false),
                ("{0} calmly slots the ball into the corner of the net, displaying clinical finishing.", true),
                ("{0} latches onto a through ball and coolly finishes past the onrushing goalkeeper.", true),
                ("{0} takes on multiple defenders and fires a low shot into the bottom corner.", true),
                ("{0} drills a powerful shot into the roof of the net from close range.", true),
                ("{0} pounces on a loose ball in the box and buries it into the back of the net.", true),
                ("{0} scores with a precise volley after a pinpoint cross from the wing.", true),
            };

            public readonly List<(string message, bool retainsPossession)> PassingMessages = new List<(string, bool)>
            {
                ("{0} delivers a pinpoint pass from midfield, splitting the defense.", true),
                ("{0} executes a lofted pass over the defense, but fails to find a teammate's run.", false),
                ("{0} plays a quick one-two combination, moving the ball swiftly through the midfield.", true),
                ("{0} switches play with a precise cross-field pass, changing the point of attack.", true),
                ("{0} delivers a curved cross into the box, looking for a teammate's head.", true),
                ("{0} plays a clever backheel pass to set up a scoring opportunity.", true),
                ("{0} finds a teammate with a perfectly timed cutback pass from the byline.", true),
                ("{0} chips the ball over the defense to send a teammate through on goal.", true),
                ("{0} showcases their vision with a long-range diagonal pass to the wing.", true),
                ("{0} attempts a difficult pass through traffic but is intercepted by the opponent.", false),
                ("{0} loses control of the ball under pressure from the opposition.", false),
                ("{0} tries to play a long ball but misjudges the distance.", false),
                ("{0} attempts a pass, but it's intercepted by the opponent.", false),
                ("{0} goes for a through ball, but the defense reads it well and clears.", false),
                ("{0} tries a cross, but it's blocked by a sliding defender.", false),
            };

            public readonly List<(string message, bool retainsPossession)> HeadingMessages = new List<(string, bool)>
            {
                ("{0} rises above the defenders and powers a header into the goal from a corner kick.", true),
                ("{0} directs a powerful header into the top corner after a precise cross.", true),
                ("{0} meets the ball with perfect timing, glancing a header into the far post.", true),
                ("{0} displays excellent aerial ability, winning headers in both defense and attack.", true),
                ("{0} scores with a diving header, meeting the ball at the near post.", true),
                ("{0} outjumps the defenders to head the ball into the bottom corner.", true),
                ("{0} rises high to clear a dangerous cross with a defensive header.", true),
                ("{0} connects with a header, but the goalkeeper makes a fantastic save.", false),
                ("{0} nods the ball down for a teammate, creating a chance from a long ball.", true),
                ("{0} flicks a header backward, surprising the goalkeeper and finding the net.", true),
                ("{0} attempts a header but mistimes it, sending the ball off-target.", false),
                ("{0} goes for a header but is outjumped by the defender.", false),
                ("{0} challenges for a header but commits a foul in the process.", false),
                ("{0} attempts a defensive header but accidentally sends the ball towards their own goal.", false),
                ("{0} jumps for a header but collides with a teammate, leading to a missed chance.", false),
            };

            public readonly List<(string message, bool retainsPossession)> GoalMessages = new List<(string, bool)>
            {
                ("{0} rises above the defenders and powers a header into the goal from a corner kick.", true),
                ("{0} delicately chips the ball over the goalkeeper to score a delightful goal.", true),
                ("{0} calmly slots the ball into the corner of the net, displaying clinical finishing.", true),
                ("{0} latches onto a through ball and coolly finishes past the onrushing goalkeeper.", true),
                ("{0} takes on multiple defenders and fires a low shot into the bottom corner.", true),
                ("{0} drills a powerful shot into the roof of the net from close range.", true),
                ("{0} pounces on a loose ball in the box and buries it into the back of the net.", true),
                ("{0} scores with a precise volley after a pinpoint cross from the wing.", true),
                ("{0} showcases incredible dribbling skills, beating several defenders to score.", true),
                ("{0} wins the ball back with a strong tackle, then finishes with a powerful shot.", true),
                ("{0} heads the ball into the net with precision after a perfectly placed cross.", true),
                ("{0} receives a well-timed pass and calmly slots it past the goalkeeper.", true),
                ("{0} dribbles past multiple opponents before firing an unstoppable shot.", true),
                ("{0} gets on the end of a rebound and blasts it home for the goal.", true),
                ("{0} displays excellent positioning to be in the right place for the tap-in goal.", true),
                ("{0} capitalizes on a defensive mistake and capitalizes with a composed finish.", true),
                ("{0} intercepts a back pass and finishes with composure to score.", true),
                ("{0} wins a header in the box and directs it into the goal.", true),
                ("{0} takes advantage of a goalmouth scramble and pokes it home.", true),
                ("{0} strikes a powerful free-kick that beats the wall and finds the net.", true),
                ("{0} finishes off a swift team passing move with a goal.", true),
                ("{0} capitalizes on a goalkeeper's error and tucks the ball into the net.", true),
                ("{0} dribbles past the last defender and coolly slots the ball into the net.", true),
                ("{0} makes a surging run from midfield, rounds the keeper, and scores.", true),
                ("{0} outmuscles the defender and finishes with a powerful shot.", true),
            };

            public readonly List<(string message, bool retainsPossession)> GoalkeeperSaveMessages = new List<(string, bool)>
            {
                ("{0} makes an acrobatic diving save, tipping the ball over the crossbar.", true),
                ("{0} reacts quickly to parry away a powerful shot from close range.", true),
                ("{0} gets down low to block a shot, keeping the ball out of the net.", true),
                ("{0} stretches out to make a fingertip save, denying a long-range effort.", true),
                ("{0} dives to the corner and makes a superb one-handed save.", true),
                ("{0} anticipates the penalty kick and makes a fantastic diving save.", true),
                ("{0} reads the attacker's shot perfectly and makes a comfortable save.", true),
                ("{0} rushes off the goal line and smothers the ball to deny a one-on-one chance.", true),
                ("{0} punches away a dangerous high cross, clearing the danger.", true),
                ("{0} gets a strong hand to a free-kick, pushing it away from the top corner.", true),
                ("{0} makes a brave save, diving at the feet of the striker to block the shot.", true),
                ("{0} reacts instinctively to a deflected shot, parrying it away from goal.", true),
                ("{0} stays big and blocks a point-blank shot with their chest.", true),
                ("{0} reacts quickly to a close-range header, saving it with a reflex save.", true),
                ("{0} makes a spectacular leaping save to deny a powerful long-range strike.", true),
                ("{0} dives to their right and saves a penalty with a strong hand.", true),
                ("{0} spreads their body and makes a crucial save in a one-on-one situation.", true),
                ("{0} tips a curling free-kick over the wall and away from goal.", true),
                ("{0} reacts quickly to a deflection and makes an agile save.", true),
                ("{0} blocks a shot from a tight angle, covering their near post well.", true),
                ("{0} reads the attacker's intentions and comfortably saves a low shot.", true),
                ("{0} reacts with lightning reflexes to deny a close-range header.", true),
                ("{0} makes a decisive save, pushing the ball to safety with strong hands.", true),
                ("{0} dives full-stretch to palm away a powerful shot from distance.", true),
                ("{0} keeps their eye on the ball and saves a tricky bouncing shot.", true),
                ("{0} stretches to the far corner and tips the ball over the bar.", true),
            };
        }


    }
}
