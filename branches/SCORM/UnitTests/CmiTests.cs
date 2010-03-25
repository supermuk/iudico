using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IUDICO.DataModel;
using IUDICO.DataModel.DB;
using IUDICO.UnitTest.Base;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Security;

namespace IUDICO.UnitTest
{
  [TestFixture]
  public class CmiTests : TestFixtureDB
  {
    int sessionID;
    int userID;
    TblLearnerSessions currentSession;
    const string s = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
      + "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

    [TestFixtureSetUp]
    protected override void InitializeFixture()
    {
      base.InitializeFixture();
      Initialize();
    }

    void Initialize()
    {
      TblCourses course = new TblCourses
      {
        Name = "course"
      };
      ServerModel.DB.Insert(course);

      TblOrganizations organisation = new TblOrganizations
      {
        CourseRef = course.ID,
        Title = "organisation"
      };
      ServerModel.DB.Insert(organisation);

      TblResources resource = new TblResources
      {
        CourseRef = course.ID,
        Type = "bob",
        Identifier = "resource"
      };
      ServerModel.DB.Insert(resource);

      TblItems item = new TblItems
      {
        OrganizationRef = organisation.ID,
        ResourceRef = resource.ID,
        Title = "item"
      };
      ServerModel.DB.Insert(item);

      TblLearnerAttempts attempt = new TblLearnerAttempts
      {
        //bug with foreign keys!
      };
      ServerModel.DB.Insert(attempt);

      TblLearnerSessions session = new TblLearnerSessions
      {
        ItemRef = item.ID,
        LearnerAttemptRef = attempt.ID
      };
      ServerModel.DB.Insert(session);
      currentSession = session;

      TblUsers user = GetUniqueUserForTesting();
      ServerModel.DB.Insert(user);

      sessionID = session.ID;
      userID = user.ID;
    }

    private static TblUsers GetUniqueUserForTesting()
    {
      var d = DateTime.Now;
      string uname = (d.Year % 100).ToString() + d.Second + d.Millisecond + d.Ticks + new Random().Next(99);
      return new TblUsers
      {
        Email = uname + "@gmail.com",
        FirstName = "Test",
        LastName = "Test",
        Login = uname,
        PasswordHash = uname
      };      
    }

    [Test]
    public void TestSessionIDAndUserID()
    {
      Assert.IsNotNull(sessionID > 0 && userID > 0);
    }

    [Test]
    public void TestSimpleCmi()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, true);
      dataModel.SetValue("completion_status", "completed");
      dataModel.SetValue("credit", "credit");
      dataModel.SetValue("entry", "ab-initio");
      dataModel.SetValue("launch_data", "bob");

      //dataModel.SetValue("learner_id", userID);
      //dataModel.SetValue("learner_name", "Santason");

      dataModel.SetValue("location", "chkPt1.p3.f5");
      dataModel.SetValue("max_time_allowed", "105");
      dataModel.SetValue("mode", "browse");
      dataModel.SetValue("progress_measure", "0,5");
      dataModel.SetValue("scaled_passing_score", "0,5");
      dataModel.SetValue("success_status", "passed");
      dataModel.SetValue("suspend_data", "bob");
      dataModel.SetValue("time_limit_action", "continue,message");
      dataModel.SetValue("session_time", "PT1H5M");

      Assert.AreEqual(dataModel.GetValue("completion_status"), "completed");
      Assert.AreEqual(dataModel.GetValue("credit"), "credit");
      Assert.AreEqual(dataModel.GetValue("entry"), "ab-initio");
      Assert.AreEqual(dataModel.GetValue("launch_data"), "bob");
      Assert.AreEqual(dataModel.GetValue("location"), "chkPt1.p3.f5");
      Assert.AreEqual(dataModel.GetValue("max_time_allowed"), "105");
      Assert.AreEqual(dataModel.GetValue("mode"), "browse");
      Assert.AreEqual(dataModel.GetValue("progress_measure"), "0,5");
      Assert.AreEqual(dataModel.GetValue("scaled_passing_score"), "0,5");
      Assert.AreEqual(dataModel.GetValue("success_status"), "passed");
      Assert.AreEqual(dataModel.GetValue("suspend_data"), "bob");
      Assert.AreEqual(dataModel.GetValue("time_limit_action"), "continue,message");
      Assert.AreEqual(dataModel.GetValue("_version"), "1.0");
      Assert.AreEqual(dataModel.GetValue("_children"), "completion_status,credit,entry,exit,launch_data,learner_id,learner_name,location,max_time_allowed,mode,progress_measure,scaled_passing_score,success_status,suspend_data,time_limit_action,session_time,total_time");
      //Assert.AreEqual(dataModel.GetValue("total_time"), "1000");//not implemented
      //Assert.AreEqual(dataModel.GetValue("learner_id"), userID);//?
      //Assert.AreEqual(dataModel.GetValue("learner_name"), "Santason");//?

      List<int> IDs = new List<int>();
      for (int i = 1; i <= 13;i++ )
      {
        IDs.Add(i);
      }
      ServerModel.DB.Delete<TblVars>(IDs);
    }

    #region CmiDataModel ReadOnly Tests

    [Test, ExpectedException(typeof(Exception))]
    public void TestCmiCreditReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("credit", "credit");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestCmiEntryReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("entry", "ab-initio");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestCmiLaunchDataReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("launch_data", "bob");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestCmiLearnerIdReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("learner_id", "bob");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestCmiLearnerNameReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("learner_name", "bob");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestMaxTimeAllowedReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("max_time_allowed", "105");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestModeReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("mode", "review");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestScaledPassingScoreReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("scaled_passing_score", "0,5");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestTimeLimitActionReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("time_limit_action", "continue,message");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestSessionTimeWriteOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.GetValue("session_time");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestVersionReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, true);
      dataModel.SetValue("_version", "1,5");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestChildrenReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, true);
      dataModel.SetValue("_children", "bob");
    }

    [Test, ExpectedException(typeof(Exception))]
    public void TestTotalTimeReadOnly()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, true);
      dataModel.SetValue("total_time", "1000");
    }
    #endregion

    #region CmiDataModel Validation Tests

    [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCmiCreditValidate()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("credit", "lol");
    }

    [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCmiEntryValidate()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("entry", "lol");
    }

    [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCmiCompletionStatusValidate()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("completion_status", "lol");
    }

    [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCmiLocationValidate()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("location", s);
    }

    [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCmiModeValidate()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("mode", "lol");
    }

    [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCmiProgressMeasureValidate()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("progress_measure", "1,5");
    }

    [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCmiScaledPassingScoreValidate()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("scaled_passing_score", "1,5");
    }

    [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCmiSuccessStatusValidate()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("success_status", "lol");
    }

    [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestCmiTimeLimitActionValidate()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("time_limit_action", "lol");
    }
    #endregion

    [Test]
    public void TestSimpleCmiMultipleLearnerSessions()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, true);      
      dataModel.SetValue("entry", "ab-initio");
      ServerModel.DB.Insert(currentSession);
      dataModel = new CmiDataModel(currentSession.ID, userID, true);
      dataModel.SetValue("entry", "resume");
      Assert.AreEqual(dataModel.GetValue("entry"), "resume");
    }
  }  
}
