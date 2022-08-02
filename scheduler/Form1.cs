﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace scheduler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //form size 1595, 962
            string date = DateTime.Now.ToString().Remove(10, 9); //current date
            int month = Int32.Parse(date.Substring(3, 2)); //current month
            int day = Int32.Parse(date.Substring(0, 2)); //current day
            int year = Int32.Parse(date.Substring(6, 4)); //current year
            string[] monthsList = { "January", "Febuary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            


            Debug.WriteLine(DateTime.Now.AddDays(1).DayOfWeek.ToString());
            Debug.WriteLine(DateTime.DaysInMonth(2022, 08));
            Debug.WriteLine(month);
            Debug.WriteLine(day);
            Debug.WriteLine(year);
            Debug.WriteLine(date);



            //x in this loop is just represents the the day in that month 'day' the var represent the day today
            for (int x = 1; x <= DateTime.DaysInMonth(year, month); x++)
            {
                DateTime dateValue = new DateTime(year, month, x);

                if (dateValue.DayOfWeek == DayOfWeek.Tuesday || dateValue.DayOfWeek == DayOfWeek.Wednesday || dateValue.DayOfWeek == DayOfWeek.Thursday || dateValue.DayOfWeek == DayOfWeek.Saturday)
                {

                    if (x == 1)
                    {
                        checkedListBox.Items.Add(dateValue.DayOfWeek.ToString() + " " + x + "st of " + monthsList[month - 1] + " " + year);
                    }
                    else if (x == 2)
                    {
                        checkedListBox.Items.Add(dateValue.DayOfWeek.ToString() + " " + x + "nd of " + monthsList[month - 1] + " " + year);
                    }
                    else if (x == 3)
                    {
                        checkedListBox.Items.Add(dateValue.DayOfWeek.ToString() + " " + x + "rd of " + monthsList[month - 1] + " " + year);
                    }
                    else
                    {
                        checkedListBox.Items.Add(dateValue.DayOfWeek.ToString() + " " + x + "th of " + monthsList[month - 1] + " " + year);
                    }

                }
            }

        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            //https://blog.elmah.io/how-to-send-emails-from-csharp-net-the-definitive-tutorial/
            //god damn that link is helpful

            Debug.WriteLine(checkedListBox.CheckedItems.Count);
            try
            {
                string output = "";
                for (int x = 0; x < checkedListBox.CheckedItems.Count; x++)
                {
                    output = output + checkedListBox.CheckedItems[x].ToString() + "\n";
                }

                Debug.WriteLine(output);

                var smtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("codeninjasscheduler@hotmail.com", "imreallylazy!"),
                    EnableSsl = true,
                };

                smtpClient.Send("codeninjasscheduler@hotmail.com", "aidanhes@hotmail.co.uk", "Schedule", "Aidan Hester Will Be Available On The Following Dates: \n" + "\n" + output + "\n This Is An Email Auto Generated By The Code Ninjas Scheduler.");

                MessageBox.Show("Your Schedule Has Been Sent!", "Schedule Sent Successfully");

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("An Error Has Occured. Please Try Again Later.", "Error: Email Not Sent");
            }


        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            for(int x = 0; x < checkedListBox.Items.Count; x++)
            {
                checkedListBox.SetItemCheckState(x, CheckState.Checked);
            }
            
        }

        private void deselectAllButton_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < checkedListBox.Items.Count; x++)
            {
                checkedListBox.SetItemCheckState(x, CheckState.Unchecked);
            }
        }

        
    }
}
