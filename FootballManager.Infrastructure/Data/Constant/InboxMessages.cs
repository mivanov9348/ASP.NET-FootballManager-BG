namespace FootballManager.Infrastructure.Data.Constant
{
    public class InboxMessages
    {
        public class Messages
        {
            public readonly List<Func<string, string, string, int, string, double, string>> SellPlayer = new List<Func<string, string, string, int, string, double, string>>
            {
                (teamName, firstName, lastName, age, position, price) =>
                    $"{teamName} is bidding farewell to {firstName} {lastName}, who is {age} years old and played as a {position}.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{firstName} {lastName} has found a new destination. {teamName} will receive {price} coins for the deal.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{firstName} {lastName} has been sold by {teamName}. The {position} will be joining a new team at the age of {age}.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"A transfer deal has been finalized as {firstName} {lastName} moves from {teamName}. The {position} is {age} years old.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{teamName} says goodbye to {firstName} {lastName}, who is leaving the club. The {age}-year-old {position} will now play elsewhere.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"The {position} {firstName} {lastName} is on the move. {teamName} has agreed to a deal worth {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"It's official! {firstName} {lastName} will no longer be a part of {teamName}. The {age}-year-old {position} has been sold.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{teamName} has transferred out {firstName} {lastName}, a {age}-year-old {position}. The transfer fee is {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{firstName} {lastName} is leaving {teamName}. The {age}-year-old {position} has been sold for {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"Goodbye {firstName} {lastName}! {teamName} has accepted an offer for the {age}-year-old {position}, receiving {price} coins."
            };

            public readonly List<Func<string, string, string, int, string, double, string>> NewPlayer = new List<Func<string, string, string, int, string, double, string>>
            {
                (teamName, firstName, lastName, age, position, price) =>
                    $"{teamName} welcomes {firstName} {lastName}, a {age}-year-old {position}, to the team. The transfer fee is {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{teamName} has acquired a new talent: {firstName} {lastName}. The {position}, aged {age}, joins for {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{firstName} {lastName} is the latest addition to {teamName}. The {age}-year-old {position} has been signed for {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{teamName} announces the signing of {firstName} {lastName}. The {position}, aged {age}, is now part of the team for {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{firstName} {lastName} joins {teamName} as a {position}. The {age}-year-old player's transfer was completed for {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{teamName} has secured the services of {firstName} {lastName}. The {age}-year-old {position} is now a part of the team, costing {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{firstName} {lastName} is the new face at {teamName}. The {position}, aged {age}, has been signed in exchange for {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{teamName} introduces {firstName} {lastName} as the latest signing. The {age}-year-old {position} arrives for {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{firstName} {lastName} has officially joined {teamName}. The {age}-year-old {position} was signed for {price} coins.",
                (teamName, firstName, lastName, age, position, price) =>
                    $"{teamName} is delighted to welcome {firstName} {lastName} to the squad. The {age}-year-old {position} was secured for {price} coins."
            };

            public readonly  List<Func<string, int, string>> NewSeasonStart = new List<Func<string, int, string>>
            {
                (teamName, seasonNum) =>
                    $"{teamName} is gearing up for season {seasonNum}. The new season brings new challenges and opportunities.",
                (teamName, seasonNum) =>
                    $"{teamName} is ready for an exciting season {seasonNum}. The team is determined to achieve great results.",
                (teamName, seasonNum) =>
                    $"{teamName} announces its preparations for season {seasonNum}. The players are excited to get back on the field.",
                (teamName, seasonNum) =>
                    $"{teamName} is getting ready for another thrilling season, season {seasonNum}. The fans are eagerly waiting for the action.",
                (teamName, seasonNum) =>
                    $"{teamName} is all set for the start of season {seasonNum}. The team's hard work during the offseason will be put to the test.",
                (teamName, seasonNum) =>
                    $"{teamName} is looking forward to season {seasonNum}. The team's efforts in training and recruitment are aimed at success.",
                (teamName, seasonNum) =>
                    $"{teamName} prepares to kick off season {seasonNum}. The players are focused on achieving their goals.",
                (teamName, seasonNum) =>
                    $"{teamName} is excited to start season {seasonNum}. The team's unity and determination will lead the way.",
                (teamName, seasonNum) =>
                    $"{teamName} is ready for the challenges of season {seasonNum}. The players are raring to go and give their best.",
                (teamName, seasonNum) =>
                    $"{teamName} welcomes the arrival of season {seasonNum}. The team is determined to make it a successful one."
            };

            public readonly List<Func<string, string, int, int, int, string>> FinishMatch = new List<Func<string, string, int, int, int, string>>
            {
                (winningTeam, losingTeam, winningTeamScore, losingTeamScore, round) =>
                    $"{winningTeam} defeats {losingTeam} in round {round} with a score of {winningTeamScore}-{losingTeamScore}.",
                (winningTeam, losingTeam, winningTeamScore, losingTeamScore, round) =>
                    $"{winningTeam} emerges victorious over {losingTeam} in round {round}, with a final score of {winningTeamScore}-{losingTeamScore}.",
                (winningTeam, losingTeam, winningTeamScore, losingTeamScore, round) =>
                    $"{winningTeam} secures a win against {losingTeam} in round {round}, finishing with a score of {winningTeamScore}-{losingTeamScore}.",
                (winningTeam, losingTeam, winningTeamScore, losingTeamScore, round) =>
                    $"{winningTeam} triumphs over {losingTeam} in round {round}, closing the game with a score of {winningTeamScore}-{losingTeamScore}.",
                (winningTeam, losingTeam, winningTeamScore, losingTeamScore, round) =>
                    $"{winningTeam} claims victory against {losingTeam} in round {round}, ending with a score of {winningTeamScore}-{losingTeamScore}.",
                (winningTeam, losingTeam, winningTeamScore, losingTeamScore, round) =>
                    $"{winningTeam} prevails over {losingTeam} in round {round}, finishing with a score of {winningTeamScore}-{losingTeamScore}.",
                (winningTeam, losingTeam, winningTeamScore, losingTeamScore, round) =>
                    $"{winningTeam} dominates {losingTeam} in round {round}, concluding with a score of {winningTeamScore}-{losingTeamScore}.",
                (winningTeam, losingTeam, winningTeamScore, losingTeamScore, round) =>
                    $"{winningTeam} comes out on top against {losingTeam} in round {round}, achieving a score of {winningTeamScore}-{losingTeamScore}.",
                (winningTeam, losingTeam, winningTeamScore, losingTeamScore, round) =>
                    $"{winningTeam} secures victory over {losingTeam} in round {round}, with a score of {winningTeamScore}-{losingTeamScore}.",
                (winningTeam, losingTeam, winningTeamScore, losingTeamScore, round) =>
                    $"{winningTeam} earns a win against {losingTeam} in round {round}, finishing with a score of {winningTeamScore}-{losingTeamScore}."
            };

            public readonly List<Func<string, string, int, int, int, string>> DrawMatch = new List<Func<string, string, int, int, int, string>>
            {
                (team1, team2, score1, score2, round) =>
                    $"{team1} and {team2} finish round {round} with a draw, both teams scoring {score1}-{score2}.",
                (team1, team2, score1, score2, round) =>
                    $"{team1} and {team2} share the points in round {round}, the match ending in a {score1}-{score2} draw.",
                (team1, team2, score1, score2, round) =>
                    $"{team1} and {team2} settle for a draw in round {round}, with both teams scoring {score1}-{score2}."
            };
        }
    }
}
