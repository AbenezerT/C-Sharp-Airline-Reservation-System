using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Airline_Reservation_System
{
    public partial class Booking : Form
    {
        //Declaring variables
        bool seatA0Booked, seatA1Booked, seatA2Booked, seatB0Booked, seatB1Booked, seatB2Booked, seatC0Booked, seatC1Booked, seatC2Booked, seatD0Booked, seatD1Booked, seatD2Booked, seatE0Booked, seatE1Booked, seatE2Booked;
        string[,] bookings;
        string[] waitingList;
        int counter;
        int customersWaitingCounter;

        /**
         * The class constructor
         * */
        public Booking()
        {
            InitializeComponent();
            bookings = new string[15, 2];
            waitingList = new string[10];
            counter = 0;
            customersWaitingCounter = 0;
            seatA0Booked = seatA1Booked = seatA2Booked = seatB0Booked = seatB1Booked = seatB2Booked = seatC0Booked = seatC1Booked = seatC2Booked = seatD0Booked = seatD1Booked = seatD2Booked = seatE0Booked = seatE1Booked = seatE2Booked = false;
        }

        private void bookButton_Click(object sender, EventArgs e)
        {
            string row = rowListBox.GetItemText(rowListBox.SelectedItem);
            string column = columnListBox.GetItemText(columnListBox.SelectedItem);
            string seat = row + column;
            if (listBoxValidated())
            {
                bool seatBooked = false;
                //check if the seat has already been booked
                for (int i = 0; i < counter; i++)
                {
                    //bookings[i, 0] seat
                    if (bookings[i, 0].Equals(seat))
                    {
                        seatBooked = true;
                    }
                }
                //seat booked already
                if (seatBooked)
                {
                    showMessageBox("The seat has already been booked");
                }
                //book the seat
                else
                {
                    bookingAndCancellation(seat);
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            string row = rowListBox.GetItemText(rowListBox.SelectedItem);
            string column = columnListBox.GetItemText(columnListBox.SelectedItem);
            string seat = row + column;
            if (listBoxValidated())
            {
                cancelBooking(seat);
            }
        }

        /**
         * The function checks if the list boxes have been selected 
         * */
        private bool listBoxValidated()
        {
            string row = rowListBox.GetItemText(rowListBox.SelectedItem);
            string column = columnListBox.GetItemText(columnListBox.SelectedItem);
            if (String.IsNullOrEmpty(row))
            {
                showMessageBox("Select the row");
                return false;
            }
            else if (String.IsNullOrEmpty(column))
            {
                showMessageBox("Select the column");
                return false;
            }
            else
            {
                return true;
            }
        }

        /**
         * The function converts a list to a 2d array of string
         * */
        static T[,] CreateRectangularArray<T>(IList<T[]> arrays)
        {
            int rowLength = arrays[0].Length;
            T[,] ret = new T[15, 2];
            for (int i = 0; i < arrays.Count; i++)
            {
                var array = arrays[i];
                if (array.Length != rowLength)
                {
                    throw new ArgumentException
                        ("All arrays must be the same length");
                }
                //copy the items of the current row to the array
                for (int j = 0; j < rowLength; j++)
                {
                    ret[i, j] = array[j];
                }
            }
            return ret;
        }

        /**
         * The function cancels the booking of the seat passed in it's parameter
         * */
        private void cancelBooking(string seatNumber)
        {
            bool cancelled = false;
            bool replaced = false;
            //check if the seat has already been booked
            for (int i = 0; i < counter; i++)
            {
                //comparing seat numbers
                if (bookings[i, 0].Equals(seatNumber))
                {
                    //replace the customer name with one from the waiting list
                    if (customersWaitingCounter != 0)
                    {
                        bookings[i, 1] = waitingList[0];
                        string[] withoutFirstName = new string[waitingList.Length];
                        Array.Copy(waitingList, 1, withoutFirstName, 0, waitingList.Length - 1);
                        waitingList = withoutFirstName;
                        customersWaitingCounter--;
                        replaced = true;
                    }
                    else
                    {
                        //create a list to hold the items on the array
                        List<string[]> newList = new List<string[]>();
                        for (int j = 0; j < counter; j++)
                        {
                            string[] temp = new string[bookings.GetLength(1)];
                            for (int n = 0; n < temp.Length; n++)
                            {
                                temp[n] = bookings[j, n];
                            }
                            newList.Add(temp);
                        }
                        //change the status of the button
                        bookingAndCancellation(bookings[i, 0]);
                        //remove the booking
                        newList.RemoveAt(i);
                        cancelled = true;
                        counter--;
                        if (newList.Count == 0)
                        {
                            bookings = new string[15, 2];
                        }
                        else
                        {
                            bookings = CreateRectangularArray(newList);
                        }
                    }
                }
            }
            if (!cancelled && replaced)
            {
                showMessageBox("The seat has been replaced with another passenger");
            }
            if (!cancelled && !replaced)
            {
                showMessageBox("The seat has not been booked yet");
            }
            if (cancelled && !replaced)
            {
                showMessageBox("The seat has been cancelled successfully");
            }
        }

        /**
         * The function handles booking and cancellation of the seats
         * */
        private void bookingAndCancellation(string seat)
        {
            if (seat.Equals("A0"))
            {
                seatA0Book();
            }
            else if (seat.Equals("A1"))
            {
                seatA1Book();
            }
            else if (seat.Equals("A2"))
            {
                seatA2Book();
            }
            else if (seat.Equals("B0"))
            {
                seatB0Book();
            }
            else if (seat.Equals("B1"))
            {
                seatB1Book();
            }
            else if (seat.Equals("B2"))
            {
                seatB2Book();
            }
            else if (seat.Equals("C0"))
            {
                seatC0Book();
            }
            else if (seat.Equals("C1"))
            {
                seatC1Book();
            }
            else if (seat.Equals("C2"))
            {
                seatC2Book();
            }
            else if (seat.Equals("D0"))
            {
                seatD0Book();
            }
            else if (seat.Equals("D1"))
            {
                seatD1Book();
            }
            else if (seat.Equals("D2"))
            {
                seatD2Book();
            }
            else if (seat.Equals("E0"))
            {
                seatE0Book();
            }
            else if (seat.Equals("E1"))
            {
                seatE1Book();
            }
            else if (seat.Equals("E2"))
            {
                seatE2Book();
            }
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            showAllBookings();
        }

        /**
         * Show all bookings customer name and seat number
         * */
        private void showAllBookings()
        {
            string bookingsStr = "";
            for (int i = 0; i < counter; i++)
            {
                bookingsStr = bookingsStr + bookings[i, 0] + " " + bookings[i, 1] + "\n";
            }
            //no bookings
            if (bookingsStr.Length == 0)
            {
                bookingsStr = "No bookings yet";
            }
            showAllArea.Text = bookingsStr;
        }

        private void seatD2Button_Click(object sender, EventArgs e)
        {
            if (seatD2Booked)
            {
                cancelBooking("D2");
            }
            else
            {
                seatD2Book();
            }
        }

        private void seatB1Button_Click(object sender, EventArgs e)
        {
            if (seatB1Booked)
            {
                cancelBooking("B1");
            }
            else
            {
                seatB1Book();
            }
        }

        private void seatA0Button_Click(object sender, EventArgs e)
        {
            if (seatA0Booked)
            {
                cancelBooking("A0");
            }
            else
            {
                seatA0Book();
            }
        }

        private void seatA1Button_Click(object sender, EventArgs e)
        {
            if (seatA1Booked)
            {
                cancelBooking("A1");
            }
            else
            {
                seatA1Book();
            }
        }

        private void seatA2Button_Click(object sender, EventArgs e)
        {
            if (seatA2Booked)
            {
                cancelBooking("A2");
            }
            else
            {
                seatA2Book();
            }
        }

        private void seatB0Button_Click(object sender, EventArgs e)
        {
            if (seatB0Booked)
            {
                cancelBooking("B0");
            }
            else
            {
                seatB0Book();
            }
        }

        private void seatB2Button_Click(object sender, EventArgs e)
        {
            if (seatB2Booked)
            {
                cancelBooking("B2");
            }
            else
            {
                seatB2Book();
            }
        }

        private void seatC0Button_Click(object sender, EventArgs e)
        {
            if (seatC0Booked)
            {
                cancelBooking("C0");
            }
            else
            {
                seatC0Book();
            }
        }

        private void seatC1Button_Click(object sender, EventArgs e)
        {
            if (seatC1Booked)
            {
                cancelBooking("C1");
            }
            else
            {
                seatC1Book();
            }
        }

        private void seatC2Button_Click(object sender, EventArgs e)
        {
            if (seatC2Booked)
            {
                cancelBooking("C2");
            }
            else
            {
                seatC2Book();
            }
        }

        private void seatD1Button_Click(object sender, EventArgs e)
        {
            if (seatD1Booked)
            {
                cancelBooking("D1");
            }
            else
            {
                seatD1Book();
            }
        }

        private void seatE0Button_Click(object sender, EventArgs e)
        {
            if (seatE0Booked)
            {
                cancelBooking("E0");
            }
            else
            {
                seatE0Book();
            }
        }

        private void seatE1Button_Click(object sender, EventArgs e)
        {
            if (seatE1Booked)
            {
                cancelBooking("E1");
            }
            else
            {
                seatE1Book();
            }
        }

        private void seatE2Button_Click(object sender, EventArgs e)
        {
            if (seatE2Booked)
            {
                cancelBooking("E2");
            }
            else
            {
                seatE2Book();
            }
        }

        private void seatD0Button_Click(object sender, EventArgs e)
        {
            if (seatD0Booked)
            {
                cancelBooking("D0");
            }
            else
            {
                seatD0Book();
            }
        }


        /**
         * Check if the name entered is valid
         * */
        private bool validCustomerName()
        {
            if (nameTextBox.Text.Equals(""))
            {
                showMessageBox("Enter passenger name first");
                return false;
            }
            else if (nameTextBox.Text.Length < 2)
            {
                showMessageBox("Passenger name must be atleast 2 characters");
                return false;
            }
            return true;
        }

        private void statusButton_Click(object sender, EventArgs e)
        {
            string row = rowListBox.GetItemText(rowListBox.SelectedItem);
            string column = columnListBox.GetItemText(columnListBox.SelectedItem);
            string seat = row + column;
            if (listBoxValidated())
            {
                bool seatBooked = false;
                //Check if if booked or not
                for (int i = 0; i < counter; i++)
                {
                    //bookings[i, 0]
                    if (bookings[i, 0].Equals(seat))
                    {
                        seatBooked = true;
                    }
                }
                if (seatBooked)
                {
                    statusTextBox.Text = "Not available";
                }
                else
                {
                    statusTextBox.Text = "Available";
                }
            }
        }

        private void waitingButton_Click(object sender, EventArgs e)
        {
            if (counter != 15)
            {
                showMessageBox("Seats are available");
            }
            else
            {
                //some customers are waiting
                if (customersWaitingCounter != 10)
                {
                    if (validCustomerName())
                    {
                        waitingList[customersWaitingCounter] = nameTextBox.Text;
                        showMessageBox("Customer name added to waiting list");
                        customersWaitingCounter++;
                    }
                }
                else
                {
                    showMessageBox("Waiting list is already full");
                }
            }
        }

        private void showWaitingButton_Click(object sender, EventArgs e)
        {
            string waitingStr = "";
            for (int i = 0; i < customersWaitingCounter; i++)
            {
                waitingStr = waitingStr + waitingList[i] + "\n";
            }
            waitingListArea.Text = waitingStr;
        }

        private void fillAllButton_Click(object sender, EventArgs e)
        {
            //fill all the 15 seats 
            counter = 0;
            string customerName = "Abenezer Tesma";
            seatA0Button.BackColor = Color.Green;
            seatA0Booked = true;
            bookings[counter, 0] = "A0";
            bookings[counter, 1] = customerName;
            counter++;
            seatA1Button.BackColor = Color.Green;
            seatA1Booked = true;
            bookings[counter, 0] = "A1";
            bookings[counter, 1] = customerName;
            counter++;
            seatA2Button.BackColor = Color.Green;
            seatA2Booked = true;
            bookings[counter, 0] = "A2";
            bookings[counter, 1] = customerName;
            counter++;
            seatB0Button.BackColor = Color.Green;
            seatB0Booked = true;
            bookings[counter, 0] = "B0";
            bookings[counter, 1] = customerName;
            counter++;
            seatB2Button.BackColor = Color.Green;
            seatB2Booked = true;
            bookings[counter, 0] = "B2";
            bookings[counter, 1] = customerName;
            counter++;
            seatB1Button.BackColor = Color.Green;
            seatB1Booked = true;
            bookings[counter, 0] = "B1";
            bookings[counter, 1] = customerName;
            counter++;
            seatC0Button.BackColor = Color.Green;
            seatC0Booked = true;
            bookings[counter, 0] = "C0";
            bookings[counter, 1] = customerName;
            counter++;
            seatC1Button.BackColor = Color.Green;
            seatC1Booked = true;
            bookings[counter, 0] = "C1";
            bookings[counter, 1] = customerName;
            counter++;
            seatC2Button.BackColor = Color.Green;
            seatC2Booked = true;
            bookings[counter, 0] = "C2";
            bookings[counter, 1] = customerName;
            counter++;
            seatD0Button.BackColor = Color.Green;
            seatD0Booked = true;
            bookings[counter, 0] = "D0";
            bookings[counter, 1] = customerName;
            counter++;
            seatD1Button.BackColor = Color.Green;
            seatD1Booked = true;
            bookings[counter, 0] = "D1";
            bookings[counter, 1] = customerName;
            counter++;
            seatD2Button.BackColor = Color.Green;
            seatD2Booked = true;
            bookings[counter, 0] = "D2";
            bookings[counter, 1] = customerName;
            counter++;
            seatE0Button.BackColor = Color.Green;
            seatE0Booked = true;
            bookings[counter, 0] = "E0";
            bookings[counter, 1] = customerName;
            counter++;
            seatE1Button.BackColor = Color.Green;
            seatE1Booked = true;
            bookings[counter, 0] = "E1";
            bookings[counter, 1] = customerName;
            counter++;
            seatE2Button.BackColor = Color.Green;
            seatE2Booked = true;
            bookings[counter, 0] = "E2";
            bookings[counter, 1] = customerName;
            counter++;
        }

        /**
         * Handle seat A0 booking
         * */
        private void seatA0Book()
        {
            if (seatA0Booked)
            {
                seatA0Booked = false;
                seatA0Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatA0Button.BackColor = Color.Green;
                    seatA0Booked = true;
                    bookings[counter, 0] = "A0";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }

            }
        }
        /**
         * Handle seat B1 booking
         * */
        private void seatB1Book()
        {
            if (seatB1Booked)
            {
                seatB1Booked = false;
                seatB1Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatB1Button.BackColor = Color.Green;
                    seatB1Booked = true;
                    bookings[counter, 0] = "B1";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }

            }
        }
        /**
         * Handle seat A1 booking
         * */
        private void seatA1Book()
        {
            if (seatA1Booked)
            {
                seatA1Booked = false;
                seatA1Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatA1Button.BackColor = Color.Green;
                    seatA1Booked = true;
                    bookings[counter, 0] = "A1";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }

            }
        }
        /**
         * Handle seat A2 booking
         * */
        private void seatA2Book()
        {
            if (seatA2Booked)
            {
                seatA2Booked = false;
                seatA2Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatA2Button.BackColor = Color.Green;
                    seatA2Booked = true;
                    bookings[counter, 0] = "A2";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }

            }
        }

        /**
         * Handle seat B0 booking
         * */
        private void seatB0Book()
        {
            if (seatB0Booked)
            {
                seatB0Booked = false;
                seatB0Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatB0Button.BackColor = Color.Green;
                    seatB0Booked = true;
                    bookings[counter, 0] = "B0";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }

            }
        }

        /**
         * Handle seat B2 booking
         * */
        private void seatB2Book()
        {
            if (seatB2Booked)
            {
                seatB2Booked = false;
                seatB2Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatB2Button.BackColor = Color.Green;
                    seatB2Booked = true;
                    bookings[counter, 0] = "B2";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }

            }
        }

        /**
         * Handle seat C0 booking
         * */
        private void seatC0Book()
        {
            if (seatC0Booked)
            {
                seatC0Booked = false;
                seatC0Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatC0Button.BackColor = Color.Green;
                    seatC0Booked = true;
                    bookings[counter, 0] = "C0";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }
            }
        }
        /**
         * Handle seat C1 booking
         * */
        private void seatC1Book()
        {
            if (seatC1Booked)
            {
                seatC1Booked = false;
                seatC1Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatC1Button.BackColor = Color.Green;
                    seatC1Booked = true;
                    bookings[counter, 0] = "C1";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }

            }
        }
        /**
         * Handle seat C2 booking
         * */
        private void seatC2Book()
        {
            if (seatC2Booked)
            {
                seatC2Booked = false;
                seatC2Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatC2Button.BackColor = Color.Green;
                    seatC2Booked = true;
                    bookings[counter, 0] = "C2";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }

            }
        }
        /**
         * Handle seat D1 booking
         * */
        private void seatD1Book()
        {
            if (seatD1Booked)
            {
                seatD1Booked = false;
                seatD1Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatD1Button.BackColor = Color.Green;
                    seatD1Booked = true;
                    bookings[counter, 0] = "D1";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }
            }
        }
        /**
         * Handle seat E0 booking
         * */
        private void seatE0Book()
        {
            if (seatE0Booked)
            {
                seatE0Booked = false;
                seatE0Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatE0Button.BackColor = Color.Green;
                    seatE0Booked = true;
                    bookings[counter, 0] = "E0";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }

            }
        }
        /**
         * Handle seat E1 booking
         * */
        private void seatE1Book()
        {
            if (seatE1Booked)
            {
                seatE1Booked = false;
                seatE1Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatE1Button.BackColor = Color.Green;
                    seatE1Booked = true;
                    bookings[counter, 0] = "E1";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }

            }
        }

        /**
         * Handle seat E2 booking
         * */
        private void seatE2Book()
        {
            if (seatE2Booked)
            {
                seatE2Booked = false;
                seatE2Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatE2Button.BackColor = Color.Green;
                    seatE2Booked = true;
                    bookings[counter, 0] = "E2";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }

            }
        }

        /**
         * Handle seat D0 booking
         * */
        private void seatD0Book()
        {
            if (seatD0Booked)
            {
                seatD0Booked = false;
                seatD0Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatD0Button.BackColor = Color.Green;
                    seatD0Booked = true;
                    bookings[counter, 0] = "D0";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }
            }
        }

        /**
         * Handle seat D2 booking
         * */
        private void seatD2Book()
        {
            if (seatD2Booked)
            {
                seatD2Booked = false;
                seatD2Button.BackColor = Color.Cyan;
            }
            else
            {
                if (validCustomerName())
                {
                    seatD2Button.BackColor = Color.Green;
                    seatD2Booked = true;
                    bookings[counter, 0] = "D2";
                    bookings[counter, 1] = nameTextBox.Text;
                    showMessageBox("The seat has been booked successfully");
                    counter++;
                }
            }
        }

        /**
         * The function shows message in MessageBox
         * */
        private void showMessageBox(string message)
        {
            MessageBox.Show(message);
        }
    }
}
