using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.ManifestModels.MetadataModels;
using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.ResourceModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.ObjectiveModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RuleModels;
using NUnit.Framework;
using Action = IUDICO.CourseManagement.Models.ManifestModels.Action;
using File = IUDICO.CourseManagement.Models.ManifestModels.ResourceModels.File;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    [TestFixture]
    class ManifestModelsTest : BaseCourseManagementTest
    {
        [Test]
        public void MetadataTest()
        {
            var manifest = new Manifest();

            manifest.Metadata = new ManifestMetadata()
                { Metadata = new Metadata(), Schema = "schema", SchemaVersion = "v1.0" };
            manifest.Base = "base";
            manifest.Organizations.AddOrganization(new Organization());
            manifest.Organizations.Default = "1";
            manifest.Resources.ResourcesList.Add(new Resource());
            manifest.Version = "1.0";

            var path = Path.Combine(root, "manifest.xml");
            manifest.Serialize(new StreamWriter(path));
            Assert.IsTrue(System.IO.File.Exists(path));
        }

        [Test]
        public void Organizationtest()
        {
            var organization = new Organization();

            var completionThreshhold = new CompletionThreshold()
            {
                CompletedByMeasure = true,
                MinProgressMeasure = 0,
                ProgressWeight = 1
            };
            organization.CompletionThreshold = completionThreshhold;
            organization.Identifier = "id";
            organization.Items.Add(new Item("res id")
            {
                CompletionThreshold = completionThreshhold,
                Data =
                    new List<Map>(new[]
                                                                 {
                                                                     new Map
                                                                         {
                                                                             ReadSharedData = false,
                                                                             TargetID = "target id",
                                                                             WriteSharedData = false
                                                                         }
                                                                 })
            });
            organization.Metadata = new Metadata();
            organization.ObjectivesGlobalToSystem = false;
            organization.Sequencing = new Sequencing();
            organization.SharedDataGlobalToSystem = true;
            organization.Structure = "structure";
            organization.Title = "title";
            
            var manifest = new Manifest();
            manifest.Organizations.AddOrganization(organization);
            manifest.Organizations.Default = "def";

            var path = Path.Combine(root, "organization.xml");
            manifest.Serialize(new StreamWriter(path));
            Assert.IsTrue(System.IO.File.Exists(path));
        }

        [Test]
        public void ResourceTest()
        {
            var resource = new Resource();
            resource.Base = "base";
            resource.Dependencies.Add(new Dependency("ref"));
            resource.Files.Add(new File("href") { Metadata = new Metadata() });
            resource.Href = "href";
            resource.Metadata = new Metadata();
            resource.ScormType = ScormType.SCO;
            resource.Type = "web";

            var manifest = new Manifest();
            manifest.Resources.ResourcesList.Add(resource);

            var path = Path.Combine(root, "resource.xml");
            manifest.Serialize(new StreamWriter(path));
            Assert.IsTrue(System.IO.File.Exists(path));
        }

        [Test]
        public void SequencingTest()
        {
            var sequencing = new Sequencing();
            sequencing.AdlObjectives = new AdlObjectives();
            sequencing.AdlObjectives.Objectives = new List<AdlObjective>();
            sequencing.AuxiliaryResources = "res";
            sequencing.ConstrainedChoiceConsiderations = new ConstrainedChoiceConsiderations()
                                                             {
                                                                 ConstrainChoice = true,
                                                                 CourseId = 1,
                                                                 NodeId = 2,
                                                                 PreventActivation = false,
                                                                 Type = "type"
                                                             };
            sequencing.ControlMode = new ControlMode()
                                         {
                                             Choice = true,
                                             ChoiceExit = false,
                                             CourseId = 1,
                                             Flow = false,
                                             ForwardOnly = false,
                                             NodeId = 2,
                                             Type = "type",
                                             UseCurrentAttemptObjectiveInfo = false,
                                             UseCurrentAttemptProgressInfo = true
                                         };
            sequencing.DeliveryControls = new DeliveryControls
                                              {
                                                  CompletionSetByContent = true,
                                                  CourseId = 1,
                                                  NodeId = 2,
                                                  ObjectiveSetByContent = false,
                                                  Tracked = false,
                                                  Type = "type"
                                              };
            sequencing.Id = "id";
            sequencing.IdRef = "ref";
            sequencing.LimitConditions = new LimitConditions()
                                             {
                                                 AttemptAbsoluteDurationLimit = "1",
                                                 AttemptLimit = 0,
                                                 CourseId = 1,
                                                 NodeId = 2,
                                                 Type = "type"
                                             };

            sequencing.Objectives = new Objectives();
            sequencing.RandomizationControls = new RandomizationControls()
                                                   {
                                                       CourseId = 1,
                                                       NodeId = 2,
                                                       RandomizationTiming = Timing.Once,
                                                       ReorderChildren = true,
                                                       SelectCount = 1,
                                                       SelectionTiming = Timing.Never
                                                   };
            sequencing.RollupConsiderations = new RollupConsiderations
                                                  {
                                                      CourseId = 1,
                                                      NodeId = 2,
                                                      MeasureSatisfactionIfActive = false,
                                                      RequiredForCompleted = Required.IfNotSkipped,
                                                      RequiredForIncomplete = Required.IfNotSuspended,
                                                      RequiredForNotSatisfied = Required.Always,
                                                      RequiredForSatisfied = Required.IfAttempted,
                                                      Type = "type",
                                                  };
            sequencing.RollupRules = new RollupRules()
                                         {
                                             CourseId = 1,
                                             NodeId = 2,
                                             ObjectiveMeasureWeight = 1,
                                             RollupObjectiveSatisfied = false,
                                             RollupProgressCompletion = true,
                                             RollupRulesList = new List<RollupRule>()
                                         };
            sequencing.SequencingRules = new SequencingRules()
                {
                    ExitConditionRule =
                        new Rule()
                            {
                                RuleAction = new RuleAction() { Action = Action.Previous },
                                RuleConditions =
                                    new RuleConditions()
                                        {
                                            ConditionCombination = ConditionCombination.All,
                                            RuleConditionsList = new List<RuleCondition>()
                                        }
                            }
                };

            var manifest = new Manifest();
            manifest.SequencingCollection = new SequencingCollection();
            manifest.SequencingCollection.Sequencings = new List<Sequencing>();
            manifest.SequencingCollection.Sequencings.Add(sequencing);

            var path = Path.Combine(root, "sequencing.xml");
            manifest.Serialize(new StreamWriter(path));
            Assert.IsTrue(System.IO.File.Exists(path));
        }
    }
}
