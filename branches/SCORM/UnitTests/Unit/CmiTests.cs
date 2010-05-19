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
using System.Globalization;
using System.Threading;

namespace IUDICO.UnitTest.Unit
{
  [TestFixture]
  public class CmiTests : TestFixtureDB
  {
    int sessionID;
    int userID;
    TblLearnerSessions currentSession;

    [TestFixtureSetUp]
    protected override void InitializeFixture()
    {
      base.InitializeFixture();
      Initialize();
    }

	[TestFixtureTearDown]
	protected override void FinializeFixture()
	{
		base.FinializeFixture();
	}

    [SetUp]
    protected void PreTestSetUp()
    {
        //NumberFormat.NumberDecimalSeparator must be ','!
        CultureInfo ci = new CultureInfo("uk-UA");
        Thread.CurrentThread.CurrentCulture = ci;
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
    public void SessionIDAndUserIDTest()
    {
      Assert.IsNotNull(sessionID > 0 && userID > 0);
    }

    #region MainCmiTests
    [Test]
    public void SimpleCmiTest()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, true);
      dataModel.SetValue("completion_status", "completed");
      dataModel.SetValue("credit", "credit");
      dataModel.SetValue("entry", "ab-initio");
      dataModel.SetValue("launch_data", "bob");
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
      //Assert.AreEqual(dataModel.GetValue("total_time"), "1000");//not implemented, and as a result session_time isn't needed
      //Assert.AreEqual(dataModel.GetValue("learner_id"), userID);//?
      //Assert.AreEqual(dataModel.GetValue("learner_name"), "Santason");//?

      //clearing database
      List<int> IDs = new List<int>();
      for (int i = 1; i <= 13;i++ )
      {
        IDs.Add(i);
      }
      ServerModel.DB.Delete<TblVars>(IDs);
    }

    [Test]
    public void InteractionCmiTest()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("interactions.0.id", "test1");
      dataModel.SetValue("interactions.0.type", "choice");
      dataModel.SetValue("interactions.0.timestamp", "2003-07-25T03:00:00");
      dataModel.SetValue("interactions.0.weighting", "1,0");
      dataModel.SetValue("interactions.0.learner_response", "true");
      dataModel.SetValue("interactions.0.result", "010");
      dataModel.SetValue("interactions.0.latency", "PT5M");
      dataModel.SetValue("interactions.0.description", "Which of the following are red?");

      Assert.AreEqual(dataModel.GetValue("interactions.0.id"), "test1");
      Assert.AreEqual(dataModel.GetValue("interactions.0.type"), "choice");
      Assert.AreEqual(dataModel.GetValue("interactions.0.timestamp"), "2003-07-25T03:00:00");
      Assert.AreEqual(dataModel.GetValue("interactions.0.weighting"), "1,0");
      Assert.AreEqual(dataModel.GetValue("interactions.0.learner_response"), "true");
      Assert.AreEqual(dataModel.GetValue("interactions.0.result"), "010");
      Assert.AreEqual(dataModel.GetValue("interactions.0.latency"), "PT5M");
      Assert.AreEqual(dataModel.GetValue("interactions.0.description"), "Which of the following are red?");
      Assert.AreEqual(dataModel.GetValue("interactions._children"), "id,type,timestamp,weighting,result,latency,description,learner_response");

      //clearing database
      List<int> IDs = new List<int>();
      for (int i = 1; i <= 8; i++)
      {
        IDs.Add(i);
      }
      ServerModel.DB.Delete<TblVarsInteractions>(IDs);
    }

    [Test]
    public void InteractionCorrectResponseCmiTest()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      dataModel.SetValue("interactions.0.correct_responses.0.pattern", "001");

      Assert.AreEqual(dataModel.GetValue("interactions.0.correct_responses.0.pattern"), "001");

      //clearing database
      List<int> IDs = new List<int>();
      for (int i = 1; i <= 1; i++)
      {
        IDs.Add(i);
      }
      ServerModel.DB.Delete<TblVarsInteractionCorrectResponses>(IDs);
    }
    #endregion

    #region CmiDataModel Read/Write Only Tests
    [Test]
    public void ReadOnlySimpleCmiTest()
    {
      var cmiReadOnlyTestCases = new[] { new { Key = "credit", Value = "credit", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=false},
                                         new { Key = "entry", Value = "ab-initio", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=false },
                                         new { Key = "launch_data", Value = "bob", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=false },
                                         new { Key = "learner_id", Value = "bob", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=false },
                                         new { Key = "learner_name", Value = "bob", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=false },
                                         new { Key = "max_time_allowed", Value = "105", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=false },
                                         new { Key = "mode", Value = "review", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=false },
                                         new { Key = "scaled_passing_score", Value = "0,5", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=false },
                                         new { Key = "time_limit_action", Value = "continue,message", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=false },
                                         new { Key = "_version", Value = "1,5", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=true },
                                         new { Key = "_children", Value = "bob", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=true },
                                         new { Key = "total_time", Value = "1000", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=true }
      };

      for (int i = 0; i < cmiReadOnlyTestCases.Length; i++)
      {
        CmiDataModel dataModel = new CmiDataModel(sessionID, userID, cmiReadOnlyTestCases[i].IsSystem);
        try
        {
          dataModel.SetValue(cmiReadOnlyTestCases[i].Key, cmiReadOnlyTestCases[i].Value);
          Assert.IsTrue(false);
        }
        catch (Exception e)
        {
          if (cmiReadOnlyTestCases[i].ExceptionType == e.GetType())
          {
            Assert.IsTrue(true);
          }
          else
          {
            Assert.IsTrue(false);
          }
        }
      }
    }

    [Test]
    public void ReadOnlyInteractionCmiTest()
    {
      var cmiReadOnlyTestCases = new[] { new { Key = "interactions._children", Value = "bob", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=true},
                                         new { Key = "interactions._count", Value = "5", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=true }
      };

      for (int i = 0; i < cmiReadOnlyTestCases.Length; i++)
      {
        CmiDataModel dataModel = new CmiDataModel(sessionID, userID, cmiReadOnlyTestCases[i].IsSystem);
        try
        {
          dataModel.SetValue(cmiReadOnlyTestCases[i].Key, cmiReadOnlyTestCases[i].Value);
          Assert.IsTrue(false);
        }
        catch (Exception e)
        {
          if (cmiReadOnlyTestCases[i].ExceptionType == e.GetType())
          {
            Assert.IsTrue(true);
          }
          else
          {
            Assert.IsTrue(false);
          }
        }
      }
    }

    [Test]
    public void ReadOnlyInteractionCorrectResponseCmiTest()
    {
      var cmiReadOnlyTestCases = new[] { new { Key = "interactions.0.correct_responses._count", Value = "5", ExceptionType = typeof(CmiReadWriteOnlyException), IsSystem=true }
      };

      for (int i = 0; i < cmiReadOnlyTestCases.Length; i++)
      {
        CmiDataModel dataModel = new CmiDataModel(sessionID, userID, cmiReadOnlyTestCases[i].IsSystem);
        try
        {
          dataModel.SetValue(cmiReadOnlyTestCases[i].Key, cmiReadOnlyTestCases[i].Value);
          Assert.IsTrue(false);
        }
        catch (Exception e)
        {
          if (cmiReadOnlyTestCases[i].ExceptionType == e.GetType())
          {
            Assert.IsTrue(true);
          }
          else
          {
            Assert.IsTrue(false);
          }
        }
      }
    }

    [Test]
    public void WriteOnlySimpleCmiTest()
    {
      string[] cmiWriteOnlyTestCases = new string[]
      {        
        "session_time"
      };

      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      for (int i = 0; i < cmiWriteOnlyTestCases.Length; i++)
      {
        try
        {
          dataModel.GetValue(cmiWriteOnlyTestCases[i]);
          Assert.IsTrue(false);
        }
        catch (CmiReadWriteOnlyException)
        {
          Assert.IsTrue(true);
        }
        catch
        {
          Assert.IsTrue(false);
        }
      }
    }
    #endregion

    #region CmiDataModel Validation Tests
    [Test]
    public void ValidationSimpleCmiTest()
    {
      string s = new string('a',1050);
      KeyValuePair<string, string>[] CmiValidationTestCases = new KeyValuePair<string, string>[]
      {
        new KeyValuePair<string, string>("credit", "lol"),
        new KeyValuePair<string, string>("entry", "lol"),
        new KeyValuePair<string, string>("completion_status", "lol"),
        new KeyValuePair<string, string>("location", s),
        new KeyValuePair<string, string>("mode", "lol"),
        new KeyValuePair<string, string>("progress_measure", "1,5"),
        new KeyValuePair<string, string>("scaled_passing_score", "1,5"),
        new KeyValuePair<string, string>("success_status", "lol"),
        new KeyValuePair<string, string>("time_limit_action", "lol"),
      };

      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, false);
      for (int i = 0; i < CmiValidationTestCases.Length; i++)
      {
        try
        {
          dataModel.SetValue(CmiValidationTestCases[i].Key, CmiValidationTestCases[i].Value);
          Assert.IsTrue(false);
        }
        catch (ArgumentOutOfRangeException)
        {
          Assert.IsTrue(true);
        }
        catch
        {
          Assert.IsTrue(false);
        }
      }
    }

    [Test]
    public void ValidationInteractionCmiTest()
    {
      string s1 = new string('a', 5000);
      string s2 = new string('a', 300);
      var CmiValidationTestCases = new[] { new { Key = "interactions.0.id", Value = s1, ExceptionType = typeof(ArgumentOutOfRangeException), IsSystem=false},
                                           new { Key = "interactions.0.id", Value = "", ExceptionType = typeof(ArgumentException), IsSystem=false },
                                           new { Key = "interactions.0.id", Value = "A9934_\\gdfgd%", ExceptionType = typeof(ArgumentException), IsSystem=false },
                                           new { Key = "interactions.0.type", Value = "lol", ExceptionType = typeof(ArgumentOutOfRangeException), IsSystem=false },
                                           new { Key = "interactions.0.timestamp", Value = "2203-07-25T03:00:00", ExceptionType = typeof(ArgumentOutOfRangeException), IsSystem=false },
                                           new { Key = "interactions.0.timestamp", Value = "zz2003-07-25T03:00:00", ExceptionType = typeof(FormatException), IsSystem=false },
                                           new { Key = "interactions.0.weighting", Value = "p105t", ExceptionType = typeof(ArgumentOutOfRangeException), IsSystem=false },
                                           new { Key = "interactions.0.result", Value = "bob", ExceptionType = typeof(ArgumentOutOfRangeException), IsSystem=false },
                                           new { Key = "interactions.0.result", Value = "NaN", ExceptionType = typeof(ArgumentOutOfRangeException), IsSystem=false },
                                           new { Key = "interactions.0.description", Value = s2, ExceptionType = typeof(ArgumentOutOfRangeException), IsSystem=false },
                                           new { Key = "interactions.0.learner_response", Value = "NaN", ExceptionType = typeof(ArgumentOutOfRangeException), IsSystem=false },
      };
      
      for (int i = 0; i < CmiValidationTestCases.Length; i++)
      {
        CmiDataModel dataModel = new CmiDataModel(sessionID, userID, CmiValidationTestCases[i].IsSystem);
        try
        {
          dataModel.SetValue(CmiValidationTestCases[i].Key, CmiValidationTestCases[i].Value);
          Assert.IsTrue(false);
        }
        catch (Exception e)
        {   
          if(CmiValidationTestCases[i].ExceptionType==e.GetType())
          {
            Assert.IsTrue(true);
          }
          else
          {
            Assert.IsTrue(false);
          }
        }
      }
    }

    [Test, Ignore]
    public void ValidationInteractionCorrectResponseCmiTest()
    {
      Assert.IsTrue(false);
    }
    #endregion

    [Test]
    public void MultipleLearnerSessionsSimpleCmiTest()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, true);
      dataModel.SetValue("entry", "ab-initio");
      ServerModel.DB.Insert(currentSession);
      dataModel = new CmiDataModel(currentSession.ID, userID, true);
      dataModel.SetValue("entry", "resume");
      Assert.AreEqual(dataModel.GetValue("entry"), "resume");

      //clean database
      List<int> IDs = new List<int>();
      for (int i = 1; i <= 2; i++)
      {
        IDs.Add(i);
      }
      ServerModel.DB.Delete<TblVars>(IDs);
    }

    [Test]
    public void DefaultValuesCmiTest()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, true);
      Assert.AreEqual(dataModel.GetValue("completion_status"), "unknown");

      //clean database
      List<int> IDs = new List<int>();
      for (int i = 1; i <= 1; i++)
      {
        IDs.Add(i);
      }
      ServerModel.DB.Delete<TblVars>(IDs);
    }

    [Test]
    public void GetCollectionCmiTest()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, true);
      dataModel.SetValue("completion_status", "completed");
      dataModel.SetValue("credit", "credit");
      dataModel.SetValue("entry", "ab-initio");
      dataModel.SetValue("launch_data", "bob");
      dataModel.SetValue("interactions.0.id", "test1");
      dataModel.SetValue("interactions.0.type", "choice");
      dataModel.SetValue("interactions.1.type", "long-fill-in");
      dataModel.SetValue("interactions.1.weighting", "1,0");
      dataModel.SetValue("interactions.0.correct_responses.0.pattern", "110");
      dataModel.SetValue("interactions.1.correct_responses.0.pattern", "011");

      List<TblVars> collection1 = dataModel.GetCollection<TblVars>("*");
      List<TblVarsInteractions> collection2 = dataModel.GetCollection<TblVarsInteractions>("interactions.*.*");
      List<TblVarsInteractions> collection3 = dataModel.GetCollection<TblVarsInteractions>("interactions.1.*");
      List<TblVarsInteractionCorrectResponses> collection4 = dataModel.GetCollection<TblVarsInteractionCorrectResponses>("interactions.*.correct_responses.*");
      List<TblVarsInteractionCorrectResponses> collection5 = dataModel.GetCollection<TblVarsInteractionCorrectResponses>("interactions.1.correct_responses.*");

      Assert.AreEqual(collection1[0].Name, "completion_status");
      Assert.AreEqual(collection1[1].Name, "credit");
      Assert.AreEqual(collection1[2].Name, "entry");
      Assert.AreEqual(collection1[3].Name, "launch_data");
      Assert.AreEqual(collection1[0].Value, "completed");
      Assert.AreEqual(collection1[1].Value, "credit");
      Assert.AreEqual(collection1[2].Value, "ab-initio");
      Assert.AreEqual(collection1[3].Value, "bob");

      Assert.AreEqual(collection2[0].Name, "id");
      Assert.AreEqual(collection2[1].Name, "type");
      Assert.AreEqual(collection2[2].Name, "type");
      Assert.AreEqual(collection2[3].Name, "weighting");
      Assert.AreEqual(collection2[0].Value, "test1");
      Assert.AreEqual(collection2[1].Value, "choice");
      Assert.AreEqual(collection2[2].Value, "long-fill-in");
      Assert.AreEqual(collection2[3].Value, "1,0");

      Assert.AreEqual(collection3[0].Name, "type");
      Assert.AreEqual(collection3[1].Name, "weighting");
      Assert.AreEqual(collection3[0].Value, "long-fill-in");
      Assert.AreEqual(collection3[1].Value, "1,0");

      Assert.AreEqual(collection4[0].Name, "pattern");
      Assert.AreEqual(collection4[1].Name, "pattern");
      Assert.AreEqual(collection4[0].Value, "110");
      Assert.AreEqual(collection4[1].Value, "011");

      Assert.AreEqual(collection5[0].Name, "pattern");
      Assert.AreEqual(collection5[0].Value, "011");

      //clean database
      List<int> IDs = new List<int>();
      for (int i = 1; i <= 4; i++)
      {
        IDs.Add(i);
      }
      ServerModel.DB.Delete<TblVars>(IDs);
      ServerModel.DB.Delete<TblVarsInteractions>(IDs);
      ServerModel.DB.Delete<TblVarsInteractionCorrectResponses>(IDs);
    }

    #region Get Count Tests
    [Test]
    public void GetCountInteractionCmiTest()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, true);
      dataModel.SetValue("interactions.0.id", "test1");
      dataModel.SetValue("interactions.1.type", "choice");
      Assert.AreEqual(dataModel.GetValue("interactions._count"), "2");
    }

    [Test]
    public void GetCountInteractionCorrectResponseCmiTest()
    {
      CmiDataModel dataModel = new CmiDataModel(sessionID, userID, true);
      dataModel.SetValue("interactions.0.correct_responses.0.pattern", "110");
      dataModel.SetValue("interactions.0.correct_responses.1.pattern", "010");
      Assert.AreEqual(dataModel.GetValue("interactions.0.correct_responses._count"), "2");
    }
    #endregion
  }  
}
