using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BodyCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void getAge(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        int[] error = new int[] { 0, 0, 0, 0, 0 };

        private void calculateAll(object sender, EventArgs e)
        {
            int anyErrors = 0;
            string text;
            getGender();
            getAge();
            getWeight();
            getHeight();

            //Gender errors
            if(error[0] == 1)
            {
                text = "You have chosen two genders, please choose only one!";
                MessageBox.Show(text);
                anyErrors = 1;
            }

            if (error[1] == 1)
            {
                text = "You didn't choose the gender!";
                MessageBox.Show(text);
                anyErrors = 1;
            }

            //Age errors
            if(error[2] == 1)
            {
                text = "You didn't choose the age!";
                MessageBox.Show(text);
                anyErrors = 1;
            }

            //Weight erros
            if (error[3] == 1)
            {
                text = "You didn't choose the weight!";
                MessageBox.Show(text);
                anyErrors = 1;
            }

            //Height erros
            if (error[4] == 1)
            {
                text = "You didn't choose the height!";
                MessageBox.Show(text);
                anyErrors = 1;
            }

            if(anyErrors == 0)
            {
                //Calculate BMR and BFP
                var kg_cm_metric = CalculateBMI(personWeight, personHeight);
                double weightInKg = kg_cm_metric.Item1;
                double HeightInCm = kg_cm_metric.Item2;
                CalculateBMR(CalculateBFP(bmi_global), weightInKg, HeightInCm, personAge);

            }


        }

        void CalculateBMR(double bfp_result, double weight_kg, double height_cm, string persAge)
        {
            double persAgeDouble = Convert.ToDouble(persAge);
            double bmr_calculated     = 0;
            double bmr_calculated_msj = 0;
            double bmr_calculated_rhb = 0;
            double bmr_calculated_kma = 0;
            //Mifflin-St Jeor Equation
            if (personGender == 0) //Female
            {
                bmr_calculated_msj = 10 * weight_kg + 6.25 * height_cm - 5 * persAgeDouble - 161;
                msj.Text = Math.Truncate(bmr_calculated_msj).ToString();

            }
            else if(personGender == 1) //Male
            {
                bmr_calculated_msj = 10 * weight_kg + 6.25 * height_cm - 5 * persAgeDouble + 5;
                msj.Text = Math.Truncate(bmr_calculated_msj).ToString();
            }
            else
            {
                string text;
                text = "You are not a gender from the list!";
                MessageBox.Show(text);
            }


            //Revised Harris-Benedict Equation
            if (personGender == 0) //Female
            {
                bmr_calculated_rhb = (9.247 * weight_kg) + (3.098 * height_cm) - (4.330 * persAgeDouble) + 447.593;
                rhb.Text = Math.Truncate(bmr_calculated_rhb).ToString();

            }
            else if (personGender == 1) //Male
            {
                bmr_calculated_rhb = (13.397 * weight_kg) + (4.799 * height_cm) - (5.677 * persAgeDouble) + 88.362;
                rhb.Text = Math.Truncate(bmr_calculated_rhb).ToString();
            }
            else
            {
                string text;
                text = "You are not a gender from the list!";
                MessageBox.Show(text);
            }

            double lean_body_mass = weight_kg - bfp_global;
            //Katch-McArdle Formula
            bmr_calculated_kma = 370 + (21.6 * lean_body_mass);
            kma.Text = Math.Truncate(bmr_calculated_kma).ToString();

            //Average BMR
            bmr_calculated = (bmr_calculated_msj + bmr_calculated_rhb + bmr_calculated_kma) / 3;
            abmr.Text = Math.Truncate(bmr_calculated).ToString();

        }

        double bmi_global;
        public Tuple<double,double> CalculateBMI(string str_weight, string str_height)
        {
            double weight_kg = Convert.ToDouble(str_weight);
            double height_cm = Convert.ToDouble(str_height);

            double weight_pound   = weight_kg * 2.205;
            double height_inch    = height_cm / 2.54;


            double bmi_calculated = 703 * (weight_pound / (height_inch * height_inch));
            bmi_global = bmi_calculated;
            bmi.Text = Math.Truncate(bmi_calculated).ToString();

            return Tuple.Create(weight_kg, height_cm);
        }

        double bfp_global;
        double CalculateBFP(double bodyMassIndex)
        {
            int personalAge = Int16.Parse(personAge);
            double bfp_calculated = 1.2 * bmi_global + 0.23 * personalAge - 10.8 * personGender - 5.4;
            bfp.Text = Math.Truncate(bfp_calculated).ToString();
            bfp_global = bfp_calculated;
            return bfp_calculated;
        }



        string personAge;
        string personWeight;
        string personHeight;
        int    personGender;

        void getAge()
        {
            if (String.IsNullOrEmpty(age.Text))
            {
                error[2] = 1;
            }
            else
            {
                error[2] = 0;
                personAge = age.Text;
            }

        }
        void getWeight()
        {
            if (String.IsNullOrEmpty(weight.Text))
            {
                error[3] = 1;
            }
            else
            {
                error[3] = 0;
                personWeight = weight.Text;
            }
        }
        void getHeight()
        {
            if (String.IsNullOrEmpty(height.Text))
            {
                error[4] = 1;
            }
            else
            {
                error[4] = 0;
                personHeight = height.Text;
            }
        }

        bool maleGender       = false;
        bool femaleGender     = false;
        void getGender()
        {
            maleGender = male.Checked;
            femaleGender = female.Checked;

            if (maleGender && femaleGender)
            {
                error[0] = 1;
            }
            else if(!maleGender && !femaleGender)
            {
                error[1] = 1;
            }
            else
            {
                if(maleGender)
                {
                    personGender = 1; //Male
                }
                else
                {
                    personGender = 0; //Female
                }

                error[0] = 0;
                error[1] = 0;

            }

        }
    }
}
