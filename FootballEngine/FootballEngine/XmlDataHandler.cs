// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlDataHandler.cs" company="Andrew Scott">
//  Andrew Scott
// </copyright>
// <summary>
//   Class to handle parsing XML files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable PossibleNullReferenceException
namespace FootballEngine
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    /// <summary>Class to handle parsing XML files.</summary>
    public static class XmlDataHandler
    {
        /// <summary>Load a set of teams from an XML file.</summary>
        /// <param name="path">The path to the XML file.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<Team> LoadTeams(string path)
        {
            var teams = new List<Team>();
            var doc = XDocument.Load(path);
            var teamElements = doc.Element("Teams")?.Elements("Team");

            if (teamElements == null)
            {
                throw new ArgumentException("The XML file must contain teams.");
            }

            foreach (var teamElement in teamElements)
            {
                var name = teamElement.Element("Name")?.Value;
                var defence = int.Parse(teamElement.Element("Defence").Value);
                var midfield = int.Parse(teamElement.Element("Midfield").Value);
                var attack = int.Parse(teamElement.Element("Attack").Value);

                teams.Add(new Team(name, defence, midfield, attack));
            }

            return teams;
        }
    }
}