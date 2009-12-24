using System.Drawing;
using System.Web.UI;
using System.Xml;
using IUDICO.DataModel.Common;
using LEX.CONTROLS;

namespace IUDICO.DataModel.WebControl
{
    public abstract class WebControlBase
    {
        private Font _font;
        private Point _location;
        protected string _name;

        private Size _size;

        public string Name
        {
            get { return _name; }
        }

        public virtual void Parse(XmlNode node)
        {
            _name = node.Attributes["id"].Value;

            var r = new HtmlStyleReader(node);
            ReadFontAttributes(r);
            ReadPositionAttributes(r);
            ReadSizeAttributes(r);
        }

        private void ReadFontAttributes([NotNull] HtmlStyleReader styles)
        {
            /*string font_size = styles[HtmlTextWriterStyle.FontSize];
            if (!string.IsNullOrEmpty(font_size))
            {
                GraphicsUnit u = font_size.EndsWith("pt") ? GraphicsUnit.Point : GraphicsUnit.Pixel;
                FontStyle fs = FontStyle.Regular;
                if (styles[HtmlTextWriterStyle.FontWeight] == "bold")
                {
                    fs |= FontStyle.Bold;
                }
                if (styles[HtmlTextWriterStyle.FontStyle] == "italic")
                {
                    fs |= FontStyle.Italic;
                }
                font_size = font_size.Remove(font_size.Length - 2);
                font = new Font(styles[HtmlTextWriterStyle.FontFamily], float.Parse(font_size), fs, u);
            }*/
        }

        private void ReadSizeAttributes([NotNull] HtmlStyleReader styles)
        {
            if (
                !(string.IsNullOrEmpty(styles[HtmlTextWriterStyle.Width]) &&
                  string.IsNullOrEmpty(styles[HtmlTextWriterStyle.Height])))
                _size = new Size(int.Parse(styles[HtmlTextWriterStyle.Width]),
                                int.Parse(styles[HtmlTextWriterStyle.Height]));
        }

        private void ReadPositionAttributes([NotNull] HtmlStyleReader styles)
        {
            _location = new Point(int.Parse(styles[HtmlTextWriterStyle.Left]), int.Parse(styles[HtmlTextWriterStyle.Top]));
        }

        private void AddSizeAttributes([NotNull] HtmlTextWriter w)
        {
            if (_size.Width != 0 && _size.Height != 0)
            {
                w.AddStyleAttribute(HtmlTextWriterStyle.Width, string.Format("{0}px",_size.Width.ToString()));
                w.AddStyleAttribute(HtmlTextWriterStyle.Height, string.Format("{0}px",_size.Height.ToString()));
            }
        }

        private void AddPositionAttributes([NotNull] HtmlTextWriter w)
        {
            w.AddStyleAttribute(HtmlTextWriterStyle.Left, string.Format("{0}px", _location.X));
            w.AddStyleAttribute(HtmlTextWriterStyle.Top, string.Format("{0}px", _location.Y));
        }

        private void AddFontAttributes([NotNull] HtmlTextWriter w)
        {
            if (_font != null)
            {
                w.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, _font.FontFamily.Name);
                w.AddStyleAttribute(HtmlTextWriterStyle.FontSize,
                                    _font.Size + (_font.Unit == GraphicsUnit.Pixel ? "px" : "pt"));
                if (_font.Bold)
                {
                    w.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "bold");
                }
                if (_font.Italic)
                {
                    w.AddStyleAttribute(HtmlTextWriterStyle.FontStyle, "italic");
                }
            }
        }

        public virtual void Store([NotNull] HtmlTextWriter w)
        {
            w.AddAttribute(HtmlTextWriterAttribute.Id, _name);
            w.AddStyleAttribute(HtmlTextWriterStyle.Position, "absolute");
            AddSizeAttributes(w);
            AddFontAttributes(w);
            AddPositionAttributes(w);
        }
    }
}