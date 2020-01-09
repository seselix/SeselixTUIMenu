using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seselix.Menu
{
    class Menu
    {
        #region Variables
        private string menuName;

        private List<string> lines = new List<string>();

        private int noOfMenuLines;
        private int maxLength;
        private int displayType = 1;

        private bool displayName = true;
        private bool hasBeenFormatted = false;

        public bool clearsScreen = false;
        #endregion

        #region Constructors
        public Menu(string name)
        {
            menuName = name;
        }
        /// <summary>
        /// Constructor to use if one would like to change the display setting.
        /// </summary>
        /// <param name="name">Name of the Menu. Displayed at the top</param>
        /// <param name="displayOption">0 for no border. 1 for a border, default.</param>
        /// <param name="displayName">Default true, shows menu name above options.</param>
        public Menu(string name, int displayOption = 1, bool displayName = true)
        {
            menuName = name;
            displayType = displayOption;
            this.displayName = displayName;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Adds line to the menu by using a list. 
        /// </summary>
        /// <param name="line"></param>
        public void AddLine(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '\n')
                {
                    line = line.Remove(i, 1);
                    i = 0;
                }
            }

            lines.Add(line);
            if (maxLength < line.Length)
            {
                maxLength = line.Length;
            }
            noOfMenuLines++;
        }
        public void RemoveLine(int lineNumber)
        {
            lines.RemoveAt(lineNumber);
        }
        public void InsertEmtpyLine()
        {
            lines.Add(" ");
        }
        public void Display()
        {
            if (clearsScreen)
                Console.Clear();

            if (!hasBeenFormatted)
                FormatMenu();

            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine(lines[i]);
            }

        }
        private void FormatMenu()
        {
            hasBeenFormatted = true;    

            // Adds line numbers
            int lineCounter = 1;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i] == " ")
                    continue;

                lines[i] = lines[i].Insert(0, lineCounter + ". ");
                lineCounter++;
                if (maxLength < lines[i].Length)
                {
                    maxLength = lines[i].Length;
                }
            }

            // Adds menu name
            if (displayName)
            {
                lines.Insert(0, menuName);
                if (maxLength < menuName.Length)
                {
                    maxLength = menuName.Length;
                }
            }

            // Adds box if allowed
            if (displayType == 1)
            {
                // Loop to format every existing line in the list
                for(int i = 0; i < lines.Count; i++)
                {
                    lines[i] = " " + lines[i];
                    lines[i] = "║" + lines[i];

                    for (int j = lines[i].Length; j < maxLength + 4; j++)
                    {
                        lines[i] += " ";
                    }


                    lines[i] += " ";
                    lines[i] += "║";

                }

                //add top bar and bottom bar
                for (int i = 0; i < lines.Count; i += lines.Count - 1)
                {
                    if (i == 0)
                    {
                        lines.Insert(0, "╔");
                        for (int j = 1; j < maxLength + 5; j++)
                        {
                            lines[i] += "═";
                        }
                        lines[i] += "╗";
                    }
                    else if (i == lines.Count - 1)
                    {
                        lines.Insert(i + 1, "╚");
                        for (int j = 1; j < maxLength + 5; j++)
                        {
                            lines[i + 1] += "═";
                        }
                        lines[i + 1] += "╝";
                    }
                }

            }

        }
        #endregion

        #region Getters
        public string GetName() { return menuName; }
        public int GetNumberOfLines() { return noOfMenuLines;  }
        #endregion
    }
}
