﻿using RingtoneComposer.Core;
using RingtoneComposer.Core.Converter;
using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace RingtoneComposer.WinForm
{
    public partial class Form1 : Form
    {
        const int sampleRate = 44100;
        private RttlConverter serializer;
        private IOscillator oscillator;
        private SoundGenerator soundGenerator;

        public Form1()
        {
            InitializeComponent();

            serializer = new RttlConverter();
            oscillator = new SawToothOccilator(sampleRate);
            soundGenerator = new SoundGenerator(oscillator);
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            var partition = textBoxPartition.Text;

            if (string.IsNullOrEmpty(partition))
                return;

            var tune = serializer.Parse(partition);
            if (tune == null)
                return;
            
            using(var stream = soundGenerator.TuneToWaveStream(tune))
            using (var player = new SoundPlayer(stream))
            {
                player.Play();
            }

            string output = serializer.ToString(tune);

            textBoxPartition.Text = output;
        }
    }
}
