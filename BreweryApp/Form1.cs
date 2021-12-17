using Brewery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreweryApp
{
    public partial class Brewery : Form
    {
        //Global variables
        IEnumerable<global::Brewery.Models.Alcohol> data;
        int nextIndex = 0;
        decimal price = 0;

        public Brewery()
        {
            InitializeComponent();
        }

        private void FailedSearch()     //Hides all beer data on failed search
        {
            pageStatus.Visible = false;
            prevButton.Visible = false;
            nextButton.Visible = false;
            addButton.Visible = false;
            pictureBox1.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            labelname.Visible = false;
            labeltagline.Visible = false;
            labeldesc.Visible = false;
            labelabv.Visible = false;
            labelph.Visible = false;
            labeldate.Visible = false;
        }

        private void OKSearch()     //Shows all beer data on failed search
        {
            pageStatus.Visible = true;
            prevButton.Visible = true;
            nextButton.Visible = true;
            addButton.Visible = true;
            pictureBox1.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            labelname.Visible = true;
            labeltagline.Visible = true;
            labeldesc.Visible = true;
            labelabv.Visible = true;
            labelph.Visible = true;
            labeldate.Visible = true;
        }

        private void results(IEnumerable<global::Brewery.Models.Alcohol> dataresults)
        {       //Gets beer data and displays it
            try
            {
                OKSearch();
                string image_url = "https://vlr.lt/wp-content/uploads/2016/02/no-image-available.jpg";
                labelname.Text = data.First().name;
                labeltagline.Text = data.First().tagline;
                labeldesc.Text = data.First().description.GetUntilOrEmpty(".");
                labelabv.Text = data.First().abv.ToString();
                labelph.Text = data.First().ph.ToString();
                labeldate.Text = data.First().first_brewed;
                
                //Displays image
                image_url = data.First().image_url;
                string sURL = image_url;
                WebRequest req = WebRequest.Create(sURL);
                WebResponse res = req.GetResponse();
                Stream imgStream = res.GetResponseStream();
                Image img1 = Image.FromStream(imgStream);
                imgStream.Close();

                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = img1;

                //Adds  pages in status bar
                pageStatus.Text = $"Page: 1/{data.Count()}";

            }
            catch (Exception)
            {
                FailedSearch();
                MessageBox.Show("The beer you are looking for is not here!", "Sorry...",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
            }
        }

        private void nextResults(IEnumerable<global::Brewery.Models.Alcohol> dataresults, int i, int prevOrNext)
        {       //Walks back and foward between beer data
            try
            {
                string image_url = "https://vlr.lt/wp-content/uploads/2016/02/no-image-available.jpg";
                labelname.Text = data.ElementAt(i).name;
                labeltagline.Text = data.ElementAt(i).tagline;
                labeldesc.Text = data.ElementAt(i).description.GetUntilOrEmpty(".");
                labelabv.Text = data.ElementAt(i).abv.ToString();
                labelph.Text = data.ElementAt(i).ph.ToString();
                labeldate.Text = data.ElementAt(i).first_brewed;

                //Displays image
                image_url = data.ElementAt(i).image_url;
                string sURL = image_url;
                WebRequest req = WebRequest.Create(sURL);
                WebResponse res = req.GetResponse();
                Stream imgStream = res.GetResponseStream();
                Image img1 = Image.FromStream(imgStream);
                imgStream.Close();

                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = img1;

                //Navigates pages
                nextIndex++;
                pageStatus.Text = $"Page: {nextIndex.ToString()}/{ data.Count()}";
                nextIndex--;

            }
            catch (Exception)
            {
                MessageBox.Show($"No more beers matching your search!", "Sorry...",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
                //Corrects pages
                if (prevOrNext < 0) nextIndex++;
                else nextIndex--;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Clear data from provider,Next/Prev buttons and gets a new provider
            data = null;
            nextIndex = 0;
            BreweryAlcoholProvider provider = new BreweryAlcoholProvider();

            

            //Declare variables
            string name = beernamesearch.Text;
            float abv = 0.0f;
            float min_abv = 0.0f;
            float max_abv = 0.0f;
            string brewed_before = "";
            string brewed_after = "";

            string[] abv_split = beerabvsearch.Text.Replace(',', '.').Split('|');
            string[] date_split = beerdatesearch.Text.Split('|');

            //Split abv from 2.5|5 to min_abv = 2.5 and max_abv = 5 OR from 4 to abv = 4
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    min_abv = float.Parse(abv_split[0]);
                    max_abv = float.Parse(abv_split[1]);
                }
                catch (Exception ex)
                {
                    if (ex is IndexOutOfRangeException)
                    {
                        if(min_abv != 0.0F) abv = min_abv;
                        else abv = 0;

                        break;
                    }
                }
            }

            //Split date string from 12-2000|12-2999 to brewed_after = 12-2000 and brewed_before 12-2999
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    brewed_after = date_split[0];
                    brewed_before = date_split[1];
                }
                catch (IndexOutOfRangeException)
                {
                    brewed_before = "12-9999";
                }
            }

            //Does a query to API dependent on search terms used
            if (beernamesearch.Text != "" &&        //by all
               beerabvsearch.Text != "" && 
               beerdatesearch.Text != "")
            {
                data = provider.GetAlcoholByAll(name, min_abv, max_abv, abv, brewed_before, brewed_after);
                results(data);
            }
            else if (beernamesearch.Text == "" &&       //by abv and date
                     beerabvsearch.Text != "" &&
                     beerdatesearch.Text != "")
            {
                data = provider.GetAlcoholByAbvAndDate(brewed_before, brewed_after, min_abv, max_abv, abv);
                results(data);
            }
            else if (beernamesearch.Text != "" &&       //by name and date
                     beerabvsearch.Text == "" &&
                     beerdatesearch.Text != "")
            {
                data = provider.GetAlcoholByDateAndName(name, brewed_before, brewed_after);
                results(data);
            }
            else if (beernamesearch.Text != "" &&       //by name and abv
                     beerabvsearch.Text != "" &&
                     beerdatesearch.Text == "")
            {
                data = provider.GetAlcoholByAbvAndName(name, min_abv, max_abv, abv);
                results(data);
            }
            else if (beernamesearch.Text == "" &&       //by date
                     beerabvsearch.Text == "" &&
                     beerdatesearch.Text != "")
            {
                data = provider.GetAlcoholByDate(brewed_before, brewed_after);
                results(data);

            }
            else if (beernamesearch.Text == "" &&       //by abv
                     beerabvsearch.Text != "" &&
                     beerdatesearch.Text == "")
            {
                data = provider.GetAlcoholByAbv(min_abv, max_abv, abv);
                results(data);
            }
            else if (beernamesearch.Text != "" &&       //by name
                     beerabvsearch.Text == "" &&
                     beerdatesearch.Text == "")
            {
                data = provider.GetAlcoholByName(name);
                results(data);
            }
        }

        private void nextButton_Click(object sender, EventArgs e)   //Moves foward in beer data
        {
            //Used for knowing if next or prev button was used
            int prevOrNext = 1;
            nextResults(data, ++nextIndex, prevOrNext);
        }

        private void prevButton_Click(object sender, EventArgs e)   //Moves backwards in beer data
        {
            //Used for knowing if next or prev button was used
            int prevOrNext = -1;
            nextResults(data, --nextIndex, prevOrNext);
        }

        private void addButton_Click_1(object sender, EventArgs e)
        {
            //add items to shopping cart
            ListViewItem item = shoppingList.FindItemWithText(labelname.Text);

            if (item != null)   //Check if added item is already in a cart, than add to amount if it is
            {
                try
                {
                    int amount = int.Parse(item.SubItems[1].Text);
                    amount++;
                    item.SubItems[1].Text = amount.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show($"Failed to add beer to shopping cart!", "Oops!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            else
            {
                shoppingList.Items.Add(new ListViewItem(new string[] {labelname.Text, "1" ,
                $"{((data.ElementAt(nextIndex).ph + data.ElementAt(nextIndex).abv) / 2 ?? 5)}$"}));
            }
            //Calculate and add new Total cost
            price = price + (data.ElementAt(nextIndex).ph + data.ElementAt(nextIndex).abv) / 2 ?? price + 5;
            priceLabel.Text = $"{price}$";

        }

        private void removeAllButton_Click(object sender, EventArgs e)
        {
            //revome all items
            shoppingList.Items.Clear();
            //set total price to 0
            price = 0;
            priceLabel.Text = "0$";
        }

        private void removeSelectedButton_Click(object sender, EventArgs e)
        {
            // To remove currently checked item:  
            while (shoppingList.CheckedItems.Count > 0)
            {
                ListViewItem item = shoppingList.CheckedItems[0];
                //Total cost (price) = price - (beer price * beer amount)
                price = price -
                    (decimal.Parse(item.SubItems[2].Text.GetUntilOrEmpty("$")) *
                     decimal.Parse(item.SubItems[1].Text));

                priceLabel.Text = $"{price}$";
                shoppingList.Items.RemoveAt(shoppingList.CheckedIndices[0]);
            }
        }

        private void buyButton_Click(object sender, EventArgs e)
        {
            //Checks if cart is empty
            if(price != 0)
            {
                var answer = MessageBox.Show($"Are you sure you want to buy for {price}$ ?", "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (answer == DialogResult.Yes)
                {
                    MessageBox.Show($"You paid {price}$ ! \n Thanks for buying! See you soon!", "Purchase successful!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                    Application.Exit();
                }
            }
            else
            {
                var answer = MessageBox.Show($"You don't have anything in your cart!", "Oops...",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }


    static class Helper     //Reads a string until a symbol stopAt
    {
        public static string GetUntilOrEmpty(this string text, string stopAt)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }
    }
}
