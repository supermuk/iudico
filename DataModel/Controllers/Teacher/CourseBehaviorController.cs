using System;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers.Teacher
{
    public class CourseBehaviorController : ControllerBase
    {
        [ControllerParameter]
        public int CourseId;

        public readonly IVariable<string> CourseName = string.Empty.AsVariable();
        
        private const string PageOrderListId = "PageOrder";
        private const string PageCountToShowListId = "PageCountToShow";
        private const string MaxCountToSubmitListId = "MaxCountToSubmit";
        private const string All = "All";
        private const string None = "None";

        public Table CourseBehaviorTable{ get; set;}



        public void PageLoad(object sender, EventArgs e)
        {
            var course = ServerModel.DB.Load<TblCourses>(CourseId);
            CourseName.Value = course.Name;

            var themesIds = ServerModel.DB.LookupIds<TblThemes>(course, null);
            var themes = ServerModel.DB.Load<TblThemes>(themesIds);

            for (int i = 1; i <= themes.Count; i++ )
                AddThemeToTable(themes[i - 1], i);
        }

        public void SaveButtonClick(object sender, EventArgs e)
        {
            foreach (TableRow row in CourseBehaviorTable.Rows)
            {
                if (row.ID != null)
                {
                    var theme = ServerModel.DB.Load<TblThemes>(int.Parse(row.ID));
                    
                    theme.PageOrderRef = SetPageOrder((DropDownList)row.FindControl(PageOrderListId));
                    theme.PageCountToShow = SetPageCountToShow((DropDownList)row.FindControl(PageCountToShowListId));
                    theme.MaxCountToSubmit = SetMaxCountToSubmit((DropDownList)row.FindControl(MaxCountToSubmitListId));
                    
                    ServerModel.DB.Update(theme);
                }
            }
        }

        private void AddThemeToTable(TblThemes theme, int i)
        {
            var row = new TableRow {ID = theme.ID.ToString()};
            var number = new TableCell {Text = i.ToString(), HorizontalAlign = HorizontalAlign.Center};
            var name = new TableCell {Text = theme.Name, HorizontalAlign = HorizontalAlign.Center};
            var type = new TableCell {Text = theme.IsControl.ToString(), HorizontalAlign = HorizontalAlign.Center};

            var pageOrder = new TableCell {HorizontalAlign = HorizontalAlign.Center};
            pageOrder.Controls.Add(GetPageOrderDropDownList(theme.PageOrderRef));

            var pageCountToShow = new TableCell {HorizontalAlign = HorizontalAlign.Center};
            pageCountToShow.Controls.Add(GetPageCountToShowDropDownList(theme));

            var maxCountToSubmit = new TableCell {HorizontalAlign = HorizontalAlign.Center};
            maxCountToSubmit.Controls.Add(GetMaxCountToSubmitDropDownList(theme.MaxCountToSubmit));

            var themePages = new TableCell();
            themePages.Controls.Add(new HyperLink
            {
                Text = "Pages",
                NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new ThemePagesController
                                                                     {
                    BackUrl = string.Empty,
                    ThemeId = theme.ID
                })
            });

            row.Cells.AddRange(new[] { number, name, type, pageOrder, pageCountToShow, maxCountToSubmit, themePages });
            CourseBehaviorTable.Rows.Add(row);

        }

        private static int? SetPageOrder(DropDownList pageOrderDropDownList)
        {
            foreach (var e in Enum.GetValues(typeof(FxPageOrder)))
                if (e.ToString().Equals(pageOrderDropDownList.SelectedValue))
                    return (int)e;

            return null;
        }

        private static int? SetPageCountToShow(DropDownList pageCountToShowDropDownList)
        {
            var selectedValue = pageCountToShowDropDownList.SelectedValue;

            if(selectedValue == All)
                return null;

            return int.Parse(selectedValue);
        }

        private static int? SetMaxCountToSubmit(DropDownList maxCountToSubmitDropDownList)
        {
            var selectedValue = maxCountToSubmitDropDownList.SelectedValue;

            if (selectedValue == None)
                return null;

            return int.Parse(selectedValue);
        }

        private static DropDownList GetPageOrderDropDownList(int? pageOrderRef)
        {
            var list = new DropDownList {ID = PageOrderListId};

            foreach (var s in Enum.GetNames(typeof(FxPageOrder)))
                list.Items.Add(s);

            list.SelectedValue = ((pageOrderRef == null) ? FxPageOrder.Straight : (FxPageOrder) pageOrderRef).ToString();

            return list;
        }

        private static DropDownList GetPageCountToShowDropDownList(TblThemes theme)
        {
            var list = new DropDownList {ID = PageCountToShowListId};

            var pagesIds = ServerModel.DB.LookupIds<TblPages>(theme, null);

            for (int i = 1; i <= pagesIds.Count; i++)
                list.Items.Add(i.ToString());

            list.Items.Add(All);

            list.SelectedValue = theme.PageCountToShow == null ? All : theme.PageCountToShow.ToString();

            return list;
        }

        private static DropDownList GetMaxCountToSubmitDropDownList(int? maxCountToSubmit)
        {
            var list = new DropDownList {ID = MaxCountToSubmitListId};

            list.Items.AddRange(new[] { new ListItem("0"), new ListItem("1"),
                                        new ListItem("3"), new ListItem("5"),
                                        new ListItem("10"), new ListItem(None) });

            list.SelectedValue = maxCountToSubmit == null ? None : maxCountToSubmit.ToString();

            return list;
        }
    }

    [DBEnum("fxPageOrders")]
    enum FxPageOrder
    {
        Straight = 1,
        Random = 2
    }
}
