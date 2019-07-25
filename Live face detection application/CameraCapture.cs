﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace Live_face_detection_application
{
    public partial class CameraCapture : Form
    {
        //declaring global variables
        private Capture capture;        //takes images from camera as image frames
        private bool captureInProgress; // checks if capture is executing

        public CameraCapture()
        {
            InitializeComponent();
        }
        private void ProcessFrame(object sender, EventArgs arg)
        {

            Image<Bgr, byte> ImageFrame = capture.QueryFrame();  //line 1
            CamImageBox.Image = ImageFrame;        //line 2
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            #region if capture is not created, create it now
            if (capture == null)
            {
                try
                {
                    capture = new Capture();
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }
            #endregion

            if (capture != null)
            {
                if (captureInProgress)
                {  //if camera is getting frames then stop the capture and set button Text
                    // "Start" for resuming capture
                    btnStart.Text = "Start!"; //
                    Application.Idle -= ProcessFrame;
                }
                else
                {
                    //if camera is NOT getting frames then start the capture and set button
                    // Text to "Stop" for pausing capture
                    btnStart.Text = "Stop";
                    Application.Idle += ProcessFrame;
                }

                captureInProgress = !captureInProgress;
            }
        }
        private void ReleaseData()
        {
            if (capture != null)
                capture.Dispose();
        }

    }
}
