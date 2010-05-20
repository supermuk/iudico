using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FireFly.CourseEditor.GUI
{
    /// <summary>
    /// Implements customizable dialog box for numeric input.
    /// </summary>
    public partial class NumericDialog : Form 
    {
        #region Properties

        /// <summary>
        /// Receives value of numericUpDown control.
        /// </summary>
        public int Value
        {
            get
            {
                return (int)this.numericUpDown.Value;
            }
            set
            {
                this.numericUpDown.Value = value;
            }
        }

        /// <summary>
        /// Receives lower bound value.
        /// </summary>
        public int Minimum
        {
            get
            {
                return (int)this.numericUpDown.Minimum;
            }
            protected set
            {
                this.numericUpDown.Minimum = value;
            }
        }

        /// <summary>
        /// Receives upper bound value.
        /// </summary>
        public int Maximum
        {
            get
            {
                return (int)this.numericUpDown.Maximum;
            }
            protected set
            {
                this.numericUpDown.Maximum = value;
            }
        }

        /// <summary>
        /// Receives numericUpDown step value.
        /// </summary>
        public int Increment
        {
            get
            {
                return (int)this.numericUpDown.Increment;
            }
            protected set
            {
                this.numericUpDown.Increment = value;
            }
        }

        /// <summary>
        /// Receives title of dialog box.
        /// </summary>
        public string Title
        {
            get
            {
                return this.Text;
            }
            protected set
            {
                this.Text = value;
            }
        }

        /// <summary>
        /// Receives description, showed above numericUpDown control.
        /// </summary>
        public string Description
        {
            get
            {
                return this.descriptionLabel.Text;
            }
            protected set
            {
                this.descriptionLabel.Text = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor by default. initializes controls.
        /// </summary>
        public NumericDialog()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Constructor with parameters. Customizes numeric dialog box.
        /// </summary>
        /// <param name="minimum">Integer lower bound value.</param>
        /// <param name="maximum">Integer upper bound value.</param>
        /// <param name="increment">Integer numericUpDown step value.</param>
        /// <param name="title">String value represents title of dialog box.</param>
        /// <param name="description">String value represents description, showed above numericUpDown control.</param>
        public NumericDialog(int minimum, int maximum, int increment, string title, string description)
            :this()
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Increment = increment;
            this.Title = title;
            this.Description = description;
            this.Value = minimum;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets value from customized numeric dialog box.
        /// </summary>
        /// <param name="minimum">Integer lower bound value.</param>
        /// <param name="maximum">Integer upper bound value.</param>
        /// <param name="value">Referenced integer to set value to.</param>
        /// <param name="increment">Integer numericUpDown step value.</param>
        /// <param name="title">String value represents title of dialog box.</param>
        /// <param name="description">String value represents description, showed above numericUpDown control.</param>
        /// <returns>Boolean value 'true' if user submited value, 'false' - if canceled.</returns>
        public static bool GetValue(int minimum, int maximum, ref int value, int increment, string title, string description)
        {
            NumericDialog dialog = new NumericDialog(minimum, maximum, increment, title, description);
            dialog.Value = value;
            DialogResult dRes = dialog.ShowDialog();

            if (dRes == DialogResult.OK)
            {
                value = dialog.Value;
            }

            return dRes == DialogResult.OK;
        }

        #endregion
    }
}
