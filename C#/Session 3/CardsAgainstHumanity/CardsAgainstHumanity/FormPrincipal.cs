using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;

namespace CardsAgainstHumanity
{

    public partial class FormPrincipal : Form
    {


        public CardsAgainstHumanity.FormConnection Connection = new CardsAgainstHumanity.FormConnection();



        TcpClient client;
        //NetworkStream stream;
        NetworkStream stream;
        StreamWriter writer;
        StreamReader reader;
        Thread ctThread;
        

        string username = "";
        bool threadIsAlive = true;

        private List<Player> players = new List<Player>();

        Button[] playerCards = new Button[7];
        

        private Card[] whiteCards = new Card[8] { new Card(0, "Shrek."), new Card(1, "A half-boner."), new Card(2, "JOHN CENA."), new Card(3, "Bees?"), 
            new Card(4, "Dwarf tossing."), new Card(5, "Racism."), new Card(6, "An octorok."), new Card(7, "Menstruation.") };

        private Card[] blackCards = new Card[8] { new Card(0, "_____. ( ͡° ͜ʖ ͡°) "), new Card(1, "Lamp oil, rope, bombs. You want it? It’s yours my friend. As long as you have enough _____."), 
            new Card(2, "Oh boy! I’m so hungry, I could eat ____!"), new Card(3, "____ : DAFUQ IS THIS?"), new Card(4, "War! What is it good for?"), new Card(5, "What is Batman's guilty pleasure?"), 
            new Card(6, "__________. High five, bro."), new Card(7, "What's that smell?") };



        const int MIN_PLAYERS = 3;
        const int MAX_PLAYERS = 8;

        public FormPrincipal(string donneesDeConnection, string username, string password, string ip, string port)
        {
            InitializeComponent();
            DistribuerCartes();
            this.username = username;

            try
            {


                client = new TcpClient(ip, int.Parse(port)); 

                stream = client.GetStream();
                writer = new StreamWriter(stream);
                reader = new StreamReader(stream);

                writer.WriteLine(donneesDeConnection);
                writer.Flush();   //CEST AU MOMENT EXACT QUE LE FLUSH EST FAIT QUE LES DONNÉES SERONT ENVOYÉES!!!!
                reader.ReadLine();



                ctThread = new Thread(Update);
                ctThread.Start();

                textBoxChat.Text += "Vous pouvez écrire des messages ici" + "\r\n";
                this.Show();
                

                
                //players.Add(new Player(username));
                //Random rnd = new Random();

                //foreach (Player player in players)
                //{
                //    labelPlayer1.Text = "Player 1 : " + player.username;
                   
                //    for (int i = 0; i < player.handCards.Length; i++)
                //    {
                //        player.handCards[i] = whiteCards[rnd.Next(0, 8)];
                //        playerCards[i].Text = player.handCards[i].text;
                //    }
                //}
            }
            catch (Exception e44)
            {
                DialogResult messageErreur = MessageBox.Show(e44.Message);

            }
            

        }

         


        public void sendButton_Click(object sender, EventArgs e)
        {
            if (textToWrite.TextLength > 0)
            {
                //string contenuAEnvoyer = textToWrite.Text;
                //textToWrite.Text = "";
                //char premierChar = contenuAEnvoyer[0];
            


                try
                {

                   //writer = new StreamWriter(stream);     //Doit rester absolument
                   //reader = new StreamReader(stream);     //CA AUSSI


                    //if (Char.IsNumber(premierChar))
                    //{
                    //    writer.WriteLine(":" + contenuAEnvoyer);
                    //}
                    //else
                    //{
                                       //writer.Write("17" + contenuAEnvoyer);        

                    //    textBoxChat.Text += reader.ReadLine() + "\r\n";
                    //    writer.Flush();    //Doit rester absolument    
                    ////}

                    //contenuAEnvoyer = "";

                   
                    
                    byte[] outStream = System.Text.Encoding.ASCII.GetBytes("17" + textToWrite.Text + "$");
                    stream.Write(outStream, 0, outStream.Length);
                    stream.Flush();

                    byte[] inStream = new byte[10025];
                    stream.Read(inStream, 0, (int)client.ReceiveBufferSize);
                    string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                    textBoxChat.Text = textBoxChat.Text + Environment.NewLine + " >> " + returndata;
                    textToWrite.Text = "";
                    textToWrite.Focus();

                           
                }
                catch (Exception e44)
                {
                    DialogResult messageErreur = MessageBox.Show(e44.Message);

                }


            }

        }

        private void Update()
        {
            string message = " ";
            while (threadIsAlive)
            {

       
               
                


               textBoxChat.Text += reader.ReadLine() + "\r\n";

               //writer.WriteLine("5");
               

                //if (Char.IsNumber(nouvelUsager[0]))
                //{
                //    nouvelUsager = nouvelUsager.Remove(0, 1);
                //}
                //string usernameNouvelUsager = nouvelUsager.Substring(0, nouvelUsager.IndexOf(','));


                //players.Add(new Player(usernameNouvelUsager));


                //foreach (Player player in players)
                //{ 
                //    labelPlayer2.Text = "Player 2 : " + usernameNouvelUsager;
                //}

            }


        }

        public void PlayerManager()
        {
            string derniereRequete = "";
            string requeteActuelle = "";
        }

        public void DistribuerCartes()
        {
            playerCards[0] = userCard1;
            playerCards[1] = userCard2;
            playerCards[2] = userCard3;
            playerCards[3] = userCard4;
            playerCards[4] = userCard5;
            playerCards[5] = userCard6;
            playerCards[6] = userCard7;
        }

        private void FormPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            writer.WriteLine("3");
            threadIsAlive = false;
            writer.Close();
            reader.Close();
            client.Close();
        }

        private void ChangeUserCardState()
        {
            for(int i = 0; i < whiteCards.Length; i++)
            {
                playerCards[i].Enabled = !playerCards[i].Enabled;
            }
        }

        private void userCard1_Click(object sender, EventArgs e)
        {
            ChangeUserCardState();
        }



    }
}
