using System.Collections.Generic;
using System.Xml.Linq;

namespace FootballEngine
{
    public static class XmlDataHandler
    {
        public static List<Team> LoadTeams (string path)
        {
            var teams = new List<Team>();
            var doc = XDocument.Load(path);
            var teamElements = doc.Element("Teams").Elements("Team");

            foreach(var teamElement in teamElements)
            {
                var name = teamElement.Element("Name").Value;
                var defence = int.Parse(teamElement.Element("Defence").Value);
                var midfield = int.Parse(teamElement.Element("Midfield").Value);
                var attack = int.Parse(teamElement.Element("Attack").Value);

                teams.Add(new Team(name, defence, midfield, attack));
            }

            return teams;
        }
    }
}