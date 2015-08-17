using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms; 
using System.Diagnostics;

namespace GuiGame {

    /// <summary>
    /// A type of Trace Listener that sends its output to a ListBox.
    /// </summary>
    public class ListBoxTraceListener : TraceListener {

        private ListBox listBox;  // A reference to the listbox that we're writing to.

        private string stringToAddToListBox = "";

        private const char NEW_LINE = '\n';

        /// <summary>
        /// Parameterless constructor.
        /// Do not want the generic default constructor to be used
        /// as there is no way to set the ListBoxTraceListener's data.
        /// This replaces the compiler's generic default constructor.
        /// Pre:  none
        /// Post: ALWAYS throws an ArgumentException.
        /// </summary>
        /// <remarks>NOT TO BE USED!</remarks>
        public ListBoxTraceListener() {
            throw new ArgumentException("Parameterless constructor invalid.");
        } // end ListBoxTraceListener constructor

        /// <summary>
        /// Constructor with initialising parameters.
        /// Pre:  the existence of a ListBox on a GUI form.
        /// Post: initialised object.
        /// </summary>
        /// <param name="listBox">The ListBox that we're writing to.</param>
        public ListBoxTraceListener(ListBox listBox) {
            this.listBox = listBox;
        }

        /// <summary>
        /// Automatically collects the outputs from all Trace.WriteLine statements.
        /// Pre:  none.
        /// Post: the string s is displayed in the listBox.
        /// </summary>
        /// <param name="s"></param>
        public override void WriteLine(string s) {
            Write(s + NEW_LINE);
        } //end WriteLine

        /// <summary>
        /// Automatically collects the outputs from all Trace.Write statements.
        /// Pre:  none.
        /// Post: the string s is displayed in the listBox, once we receive a NEW_LINE.
        /// </summary>
        /// <param name="s"></param>
        public override void Write(string s) {
            stringToAddToListBox += s;

            // If we have one or more complete lines
            if (stringToAddToListBox.Contains (NEW_LINE)) {

                // Split the string into multiple lines. 
                // If NEW_LINE is found at the beginning or end of the string, 
                // then the corresponding array element contains an empty string. 
                string[] lines = stringToAddToListBox.Split(NEW_LINE);

                // Add all the lines to the listbox, except for the last one.
                // When stringToAddToListBox has a new-line at the end, 
                // the last element in lines[] will be an empty string.
                int highestLineNumber = lines.Length - 1;
                for (int i = 0; i < highestLineNumber; i++) {
                    AddToListBox(lines[i]);
                }

                // Reset stringToAddToListBox to what remains. (May be an empty string).
                stringToAddToListBox = lines[highestLineNumber];
            }
        } // end Write

        /// <summary>
        /// Adds a complete output-line to the ListBox.
        /// Pre:  none.
        /// Post: the string listBoxLine is displayed in the listBox.
        /// </summary>
        /// <param name="listBoxLine"></param>
        private void AddToListBox(string listBoxLine) {
            Debug.Assert(listBox != null, "listBox != null");
            listBox.Items.Add(listBoxLine);
        } // end AddToListBox
    }

}
