using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
[assembly : WebResource("BoxOver.boxover.js", "text/javascript")]

namespace BoxOver
{
    /// <summary>
    /// This component is based on JavaScript BoxOver v 2.1.
    /// Original code is taken from http://boxover.swazz.org/
    /// </summary>
    [DefaultProperty("ControlToValidate")]
    [ToolboxData("<{0}:BoxOver runat=server></{0}:BoxOver>")]
    [ToolboxBitmap(typeof (BoxOver))]
    [Description("BoxOver uses javascript / DHTML to show tooltips on a website.")]
    public class BoxOver : WebControl
    {
        #region Public Properties

        [TypeConverter(typeof (BoxOverControlConverter))]
        [Category("Behavior")]
        [Description("Determines the control to be added the tooltip.")]
        public string ControlToValidate
        {
            get
            {
                if (ViewState["ControlToValidate"] != null)
                    return (string) ViewState["ControlToValidate"];
                else
                    return string.Empty;
            }
            set { ViewState["ControlToValidate"] = value; }
        }

        [Category("BoxOver")]
        [Description("Specifies the header text of the tooltip.")]
        public string Header
        {
            get
            {
                if (ViewState["Header"] != null)
                    return (string) ViewState["Header"];
                else
                    return string.Empty;
            }
            set { ViewState["Header"] = value; }
        }

        [Category("BoxOver")]
        [Description("Specifies the body text of the tooltip.")]
        public string Body
        {
            get
            {
                if (ViewState["Body"] != null)
                    return (string) ViewState["Body"];
                else
                    return string.Empty;
            }
            set { ViewState["Body"] = value; }
        }

        [Category("BoxOver")]
        [Description("Forces the X-coordinate of the tooltip to stay fixed (offset is relative to the annotated HTML element).")]
        public int? FixedRelX
        {
            get
            {
                if (ViewState["FixedRelX"] != null)
                    return (int) ViewState["FixedRelX"];
                else
                    return null;
            }
            set { ViewState["FixedRelX"] = value; }
        }

        [Category("BoxOver")]
        [Description("Forces the Y-coordinate of the tooltip to stay fixed (offset is relative to the annotated HTML element).")]
        public int? FixedRelY
        {
            get
            {
                if (ViewState["FixedRelY"] != null)
                    return (int) ViewState["FixedRelY"];
                else
                    return null;
            }
            set { ViewState["FixedRelY"] = value; }
        }

        [Category("BoxOver")]
        [Description(
            "Forces the X-coordinate of the tooltip to stay fixed (X is an offset relative to the body of the HTML document).")]
        public int? FixedAbsX
        {
            get
            {
                if (ViewState["FixedAbsX"] != null)
                    return (int) ViewState["FixedAbsX"];
                else
                    return null;
            }
            set { ViewState["FixedAbsX"] = value; }
        }

        [Category("BoxOver")]
        [Description(
            "Forces the Y-coordinate of the tooltip to stay fixed (Y is an offset relative to the body of the HTML document).")]
        public int? FixedAbsY
        {
            get
            {
                if (ViewState["FixedAbsY"] != null)
                    return (int) ViewState["FixedAbsY"];
                else
                    return null;
            }
            set { ViewState["FixedAbsY"] = value; }
        }

        [Category("BoxOver")]
        [Description("Make tooltip stick to side of the window if user moves close to the side of the screen.")]
        public bool WindowLock
        {
            get
            {
                if (ViewState["WindowLock"] != null)
                    return (bool) ViewState["WindowLock"];
                else
                    return true;
            }
            set { ViewState["WindowLock"] = value; }
        }

        [Category("BoxOver")]
        [Description("Specifies CSS class for styles to be used on tooltip body.")]
        public string CssBody
        {
            get
            {
                if (ViewState["CssBody"] != null)
                    return (string) ViewState["CssBody"];
                else
                    return String.Empty;
            }
            set { ViewState["CssBody"] = value; }
        }

        [Category("BoxOver")]
        [Description("Specifies CSS class for styles to be used on tooltip header.")]
        public string CssHeader
        {
            get
            {
                if (ViewState["CssHeader"] != null)
                    return (string) ViewState["CssHeader"];
                else
                    return String.Empty;
            }
            set { ViewState["CssHeader"] = value; }
        }

        [Category("BoxOver")]
        [Description("Horizontal offset, in pixels, of the tooltip relative to the mouse cursor.")]
        public int OffsetX
        {
            get
            {
                if (ViewState["OffsetX"] != null)
                    return (int) ViewState["OffsetX"];
                else
                    return 10;
            }
            set { ViewState["OffsetX"] = value; }
        }

        [Category("BoxOver")]
        [Description("Vertical offset, in pixels, of the tooltip relative to the mouse cursor.")]
        public int OffsetY
        {
            get
            {
                if (ViewState["OffsetY"] != null)
                    return (int) ViewState["OffsetY"];
                else
                    return 10;
            }
            set { ViewState["OffsetY"] = value; }
        }

        [Category("BoxOver")]
        [Description("Specifies whether to halt the tooltip when the user double clicks on the HTML element with the tooltip.")]
        public bool DoubleClickStop
        {
            get
            {
                if (ViewState["DoubleClickStop"] != null)
                    return (bool) ViewState["DoubleClickStop"];
                else
                    return true;
            }
            set { ViewState["DoubleClickStop"] = value; }
        }

        [Category("BoxOver")]
        [Description(
            "Specifies whether to halt the tooltip when the user single clicks on the HTML element with the tooltip. - if both singleclickstop and doubleclickstop are set to \"true\", singleslclickstop takes preference."
            )]
        public bool SingleClickStop
        {
            get
            {
                if (ViewState["SingleClickStop"] != null)
                    return (bool) ViewState["SingleClickStop"];
                else
                    return false;
            }
            set { ViewState["SingleClickStop"] = value; }
        }

        [Category("BoxOver")]
        [Description(
            "Specifies whether the user must first click the element before a tooltip appears. Intended for use on links so that information appears while the link is followed."
            )]
        public bool RequireClick
        {
            get
            {
                if (ViewState["RequireClick"] != null)
                    return (bool) ViewState["RequireClick"];
                else
                    return false;
            }
            set { ViewState["RequireClick"] = value; }
        }

        [Category("BoxOver")]
        [Description("Specifies whether to hide all SELECT boxes on page when popup is activated.")]
        public bool HideSelects
        {
            get
            {
                if (ViewState["HideSelects"] != null)
                    return (bool) ViewState["HideSelects"];
                else
                    return false;
            }
            set { ViewState["HideSelects"] = value; }
        }

        [Category("BoxOver")]
        [Description("Specifies whether to fade tooltip into visibility.")]
        public bool Fade
        {
            get
            {
                if (ViewState["Fade"] != null)
                    return (bool) ViewState["Fade"];
                else
                    return false;
            }
            set { ViewState["Fade"] = value; }
        }

        [Category("BoxOver")]
        [Description("Specifies how fast to fade in tooltip.")]
        public decimal FadeSpeed
        {
            get
            {
                if (ViewState["FadeSpeed"] != null)
                    return (decimal) ViewState["FadeSpeed"];
                else
                    return (decimal) 0.04;
            }
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException("Value of \"FadeSpeed\" must be between 0 and 1.");

                ViewState["FadeSpeed"] = value;
            }
        }

        [Category("BoxOver")]
        [Description("Specifies delay in milliseconds before tooltip displays.")]
        public int Delay
        {
            get
            {
                if (ViewState["Delay"] != null)
                    return (int) ViewState["Delay"];
                else
                    return 0;
            }
            set { ViewState["Delay"] = value; }
        }

        #endregion

        #region Public Overrided Methods

        #region Helper Methods

        private string CreateAttributes()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(String.Format("body=[{0}]", Body));
            sb.Append(String.Format(" header=[{0}]", Header == String.Empty ? "&nbsp;" : Header));

            if (FixedRelX != null)
                sb.Append(String.Format(" fixedrelx=[{0}]", FixedRelX));

            if (FixedRelY != null)
                sb.Append(String.Format(" fixedrely=[{0}]", FixedRelY));

            if (FixedAbsX != null)
                sb.Append(String.Format(" fixedabsx=[{0}]", FixedAbsX));

            if (FixedAbsY != null)
                sb.Append(String.Format(" fixedabsy=[{0}]", FixedAbsY));

            sb.Append(String.Format(" windowlock=[{0}]", WindowLock ? "on" : "off"));

            if (CssBody != String.Empty)
                sb.Append(String.Format(" cssbody=[{0}]", CssBody));

            if (CssHeader != String.Empty)
                sb.Append(String.Format(" cssheader=[{0}]", CssHeader));

            if (OffsetX != 10)
                sb.Append(String.Format(" offsetx=[{0}]", OffsetX));

            if (OffsetY != 10)
                sb.Append(String.Format(" offsety=[{0}]", OffsetY));

            sb.Append(String.Format(" doubleclickstop=[{0}]", DoubleClickStop ? "on" : "off"));
            sb.Append(String.Format(" singleclickstop=[{0}]", SingleClickStop ? "on" : "off"));
            sb.Append(String.Format(" requireclick=[{0}]", RequireClick ? "on" : "off"));
            sb.Append(String.Format(" hideselects=[{0}]", HideSelects ? "on" : "off"));
            sb.Append(String.Format(" fade=[{0}]", Fade ? "on" : "off"));

            if (FadeSpeed != (decimal) 0.04)
                sb.Append(String.Format(" fadespeed=[{0}]", FadeSpeed.ToString().Replace(",", ".")));

            if (Delay != 0)
                sb.Append(String.Format(" delay=[{0}]", Delay));

            return sb.ToString();
        }

        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Control c = FindControl(ControlToValidate);

            if (c == null)
                return;

            if (!Page.ClientScript.IsClientScriptBlockRegistered(GetType(), "BoxOver"))
                Page.ClientScript.RegisterClientScriptResource(GetType(), "BoxOver.boxover.js");

            WebControl wc = (WebControl) c;
            wc.Attributes.Add("title", CreateAttributes());
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (ControlToValidate == string.Empty)
            {
                if (DesignMode)
                {
                    writer.Write("No control selected.");
                    return;
                }
                else
                    throw new HttpException(String.Format("The 'ControlToValidate' property of '{0}' cannot be blank.", ClientID));
            }

            Control c = FindControl(ControlToValidate);

            if (c == null)
            {
                if (DesignMode)
                {
                    writer.Write("Control not found!");
                    return;
                }
                else
                    throw new HttpException(
                        String.Format("Unable to find control id '{0}' referenced by the 'ControlToValidate' property of '{1}'.",
                                      ControlToValidate, ClientID));
            }

            if (Body == String.Empty && !DesignMode)
                throw new HttpException(String.Format("The 'Body' property of '{0}' cannot be blank.", ClientID));

            if (DesignMode)
                writer.Write(String.Format("BoxOver: {0}", c.ClientID));
        }

        #endregion
    }
}