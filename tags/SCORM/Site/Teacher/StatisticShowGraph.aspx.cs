using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.Controllers.Teacher;
using System.Windows.Forms;
using System.Drawing;
using System;
using ZedGraph.Web;
using ZedGraph;

public partial class Teacher_StatisticShowGraph : ControlledPage<StatisticShowGraphController>
{
    StatisticShowGraphController d;

    protected override void BindController(StatisticShowGraphController c)
    {
        base.BindController(c);
        //Bind(Saveto_Excel, c.Saveto_Excel_Click);

        d = c;

        this.ZedGraphWeb2.RenderGraph += new ZedGraph.Web.ZedGraphWebControlEventHandler(this.OnRenderGraph);
    }
    public void OnRenderGraph(ZedGraphWeb zgw, System.Drawing.Graphics g, ZedGraph.MasterPane masterPane)
    {

        GraphPane myPane = masterPane[0];

        myPane.Title.Text = "Statistic for studet " + d.user.LastName + " for curriculum " + d.curriculum.Name; ;
        myPane.XAxis.Title.Text = "Stage";
        myPane.YAxis.Title.Text = "Count";

        PointPairList list = new PointPairList();
        PointPairList list2 = new PointPairList();


        for (int x = 0; x < d.Get_Name_Stage().Count; x++)
        {
            double y = d.Get_Student_Stage_Count()[x];
            double y2 = d.Get_Total_Stage_Count()[x];

            list.Add(x, y);
            list2.Add(x, y2);

        }

        BarItem myCurve = myPane.AddBar("Student Resoult", list, Color.Green);
        BarItem myCurve2 = myPane.AddBar("Stage Rank", list2, Color.Red);
        string[] label11 = new string[d.Get_Student_Stage_Count().Count];
        for (int x = 0; x < d.Get_Student_Stage_Count().Count; x++)
        {

            label11[x] = d.Get_Name_Stage()[x];
        }

        myPane.XAxis.MajorTic.IsBetweenLabels = true;

        myPane.XAxis.Scale.TextLabels = label11;
        myPane.XAxis.Type = AxisType.Text;
        myPane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255), 45.0f);
        myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);

        masterPane.AxisChange(g);

    }


}
