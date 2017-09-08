using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace Video_Döndürme_Çevirme
{
	public partial class MainForm : Form
	{
		string girdiYolu,ciktiKlasor,ciktiDosya;
		string konum,param;
		string vcodec,acodec;
		string thumbnailPath;
		
		public MainForm()
		{
			InitializeComponent();
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			
		}
		void Button1Click(object sender, EventArgs e)
		{
			try {
				
				OpenFileDialog file = new OpenFileDialog();  
			    //file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			    file.RestoreDirectory = true;   
				file.CheckFileExists = false;		    
				file.Title = "Video Seç.."; 
				file.Multiselect = false;
		        if (file.ShowDialog() == DialogResult.OK)  
			    {  
			        girdiYolu = file.FileName;  
			        thumbnailPath = Application.StartupPath + "\\1.jpg";
		            ProcessStartInfo startInfo = new ProcessStartInfo();
			        startInfo.CreateNoWindow = false;
			        startInfo.UseShellExecute = false;
			        startInfo.FileName = Application.StartupPath + "\\ffmpeg.exe";
			        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			        startInfo.Arguments = "-y -i " + "\"" + girdiYolu+ "\"" + " -vframes 1 " + "\"" + thumbnailPath+ "\""; 
			        
			            using (Process exeProcess = Process.Start(startInfo))
			            {
			                exeProcess.WaitForExit();
			            }
			       
					pictureBox1.ImageLocation = thumbnailPath;
					pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
					
					comboBox1.Enabled = true;
					comboBox1.SelectedIndex = -1;
					
					button2.Enabled = false;
					button3.Enabled = false;
			    }
         	} catch (Exception ex){
	        	MessageBox.Show(ex.Message.ToString());
	        }	
		}

		void Button2Click(object sender, EventArgs e)
		{
			try {
				var Klasor = new FolderBrowserDialog();  
			    Klasor.RootFolder = Environment.SpecialFolder.Desktop;  
		    	DialogResult result=Klasor.ShowDialog();
				if (result==DialogResult.OK) {
			  		ciktiKlasor = Klasor.SelectedPath;
			  		textBox1.Text = ciktiKlasor + "\\" + ciktiDosya;
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message.ToString());
			}
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			try
	        {
		        ProcessStartInfo startInfo = new ProcessStartInfo();
		        startInfo.CreateNoWindow = false;
		        startInfo.UseShellExecute = false;
		        startInfo.FileName = Application.StartupPath + "\\ffmpeg.exe";
		        startInfo.WindowStyle = ProcessWindowStyle.Normal;
		        startInfo.Arguments = "-y -i "+girdiYolu+vcodec+" "+acodec+"-vf \""+param+"\" "+ciktiKlasor+"\\"+ciktiDosya;
		        
	            using (Process exeProcess = Process.Start(startInfo)) {
	                exeProcess.WaitForExit();
	            }
	        } catch (Exception ex) {
	        	MessageBox.Show(ex.Message.ToString());
	        }
		}

		
		
		
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try {
				System.Diagnostics.Process.Start("http://www.dogusumit.com/");
			} catch (Exception ex) {
				MessageBox.Show(ex.Message.ToString());
			}
		}
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			if(comboBox1.SelectedIndex > -1)
			{
				var bitmap1 = (Bitmap)Bitmap.FromFile(thumbnailPath);
				
							switch (comboBox1.SelectedIndex) {
				case 0:
					param = "transpose=1";
					bitmap1.RotateFlip(RotateFlipType.Rotate90FlipNone);
					konum = "90";
					break;
				case 1:
					param = "transpose=2,transpose=2";
					bitmap1.RotateFlip(RotateFlipType.Rotate180FlipNone);
					konum = "180";
					break;
                case 2:
					param = "transpose=2";
					bitmap1.RotateFlip(RotateFlipType.Rotate270FlipNone);
					konum = "270";
					break;
				case 3:
					param = "vflip";
					bitmap1.RotateFlip(RotateFlipType.RotateNoneFlipY); 
					konum = "dikey";
					break;
				case 4:
					param = "hflip";
					bitmap1.RotateFlip(RotateFlipType.RotateNoneFlipX); 
					konum = "yatay";
					break;
			}
				
				ciktiDosya =  Path.GetFileNameWithoutExtension(girdiYolu) + "_" + konum + Path.GetExtension(girdiYolu); 
				textBox1.Text = "Şuraya kaydedilecek : Masaüstü\\" + ciktiDosya ;
				
				ciktiKlasor = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				
				pictureBox1.Image = bitmap1;
				button2.Enabled = true;
				button3.Enabled = true;
			}
			

	
		}
		void ComboBox2SelectedIndexChanged(object sender, EventArgs e)
		{
			if(comboBox2.SelectedIndex > -1)
			{
				if (comboBox2.SelectedIndex == 0) 
					vcodec = "";
				else
					vcodec = " -c:v " + comboBox2.SelectedItem.ToString() + " ";
			}
		}
		void ComboBox3SelectedIndexChanged(object sender, EventArgs e)
		{
				if(comboBox3.SelectedIndex > -1)
			{
				if (comboBox3.SelectedIndex == 0) 
					acodec = "";
				else
					acodec = " -c:a " + comboBox3.SelectedItem.ToString() + " ";
			}
		}
	}
}
