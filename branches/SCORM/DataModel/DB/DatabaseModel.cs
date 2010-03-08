﻿using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.DB
{
    public partial class FxCourseOperations : FxDataObject, IFxDataObject
    {
        [TableRecord]
        public readonly static FxCourseOperations Modify;

        [TableRecord]
        public readonly static FxCourseOperations Use;
    }

    public partial class FxThemeOperations : FxDataObject, IFxDataObject
    {
        [TableRecord]
        public readonly static FxThemeOperations View;

        [TableRecord]
        public readonly static FxThemeOperations Pass;
    }

    public partial class FxStageOperations : FxDataObject, IFxDataObject
    {
        [TableRecord]
        public readonly static FxStageOperations View;

        [TableRecord]
        public readonly static FxStageOperations Pass;
    }

    public partial class FxLanguages : FxDataObject, IFxDataObject
    {
        [TableRecord]
        public static readonly FxLanguages Vs6CPlusPlus;

        [TableRecord]
        public static readonly FxLanguages Vs8CPlusPlus;

        [TableRecord]
        public static readonly FxLanguages DotNet2;

        [TableRecord]
        public static readonly FxLanguages DotNet3;

        [TableRecord]
        public static readonly FxLanguages Java6;

        [TableRecord]
        public static readonly FxLanguages Delphi7;
    }

    public partial class FxRoles : FxDataObject, IFxDataObject
    {
        [TableRecord]
        public static readonly FxRoles STUDENT;
        [TableRecord]
        public static readonly FxRoles LECTOR;
        [TableRecord]
        public static readonly FxRoles TRAINER;
        [TableRecord]
        public static readonly FxRoles ADMIN;
        [TableRecord]
        public static readonly FxRoles SUPER_ADMIN;
    }

    public partial class FxGroupOperations : FxDataObject, IFxDataObject
    {
        [TableRecord]
        public readonly static FxGroupOperations View;
        [TableRecord]
        public readonly static FxGroupOperations Rename;
        [TableRecord]
        public readonly static FxGroupOperations ChangeMembers;
    }

    public partial class FxCurriculumOperations : FxDataObject, IFxDataObject
    {
        [TableRecord]
        public readonly static FxCurriculumOperations Modify;

        [TableRecord]
        public readonly static FxCurriculumOperations Use;

        [TableRecord]
        public readonly static FxCurriculumOperations View;

        [TableRecord]
        public readonly static FxCurriculumOperations Pass;
    }

    public partial class FxPageOrders : FxDataObject, IFxDataObject
    {
        [TableRecord]
        public static readonly FxPageOrders Straight;
        [TableRecord]
        public static readonly FxPageOrders Random;
    }

    public partial class FxAnswerType : FxDataObject, IFxDataObject
    {
        [TableRecord]
        public static readonly FxAnswerType UserAnswer;
        [TableRecord]
        public static readonly FxAnswerType EmptyAnswer;
        [TableRecord]
        public static readonly FxAnswerType NotIncludedAnswer;
    }

    public partial class TblPermissions : IntKeyedDataObject, IIntKeyedDataObject
    {
        public DateTimeInterval WorkingInterval
        {
            get
            {
                return new DateTimeInterval(DateSince, DateTill);
            }
            set
            {
                DateSince = value.From;
                DateTill = value.To;
            }
        }

        public int? GetObjectID(SECURED_OBJECT_TYPE ot)
        {
            return (int?)GetType().GetProperty(ot.GetSecurityAtr().Name + "Ref").GetValue(this, null);
        }

        public void SetObjectID(SECURED_OBJECT_TYPE ot, int value)
        {
            GetType().GetProperty(ot.GetSecurityAtr().Name + "Ref").SetValue(this, value, null);
        }

        public int? GetOperationID(SECURED_OBJECT_TYPE ot)
        {
            return (int?)GetType().GetProperty(ot.GetSecurityAtr().Name + "OperationRef").GetValue(this, null);
        }

        public void SetOperationID(SECURED_OBJECT_TYPE ot, int value)
        {
            GetType().GetProperty(ot.GetSecurityAtr().Name + "OperationRef").SetValue(this, value, null);
        }
    }

    public partial class TblCompiledAnswers : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCompiledQuestions : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCompiledQuestionsData : IntKeyedDataObject, IIntKeyedDataObject
    {
    }

    public partial class TblCourses : SecuredDataObject<FxCourseOperations>, ISecuredDataObject<FxCourseOperations> {}

    public partial class TblCurriculums : SecuredDataObject<FxCurriculumOperations>, ISecuredDataObject<FxCurriculumOperations>
    {
    }

    public partial class TblThemes : SecuredDataObject<FxThemeOperations>, ISecuredDataObject<FxThemeOperations> { }

    public partial class TblGroups : SecuredDataObject<FxGroupOperations>, ISecuredDataObject<FxGroupOperations> { }

    public partial class TblQuestions : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblSampleBusinesObject : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblStages : SecuredDataObject<FxStageOperations>, ISecuredDataObject<FxStageOperations> { }

    public partial class TblUserAnswers : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblOrganizations : IntKeyedDataObject, IIntKeyedDataObject { }
    
    public partial class TblItems : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblResources : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblUserNotes : IntKeyedDataObject, IIntKeyedDataObject { }

    public partial class TblUsers : IntKeyedDataObject, IIntKeyedDataObject
    {
        public string DisplayName
        {
            get { return FirstName + " " + LastName + "(" + Login + ")"; }
        }
    }

    public partial class TblLearnerAttempts : IntKeyedDataObject, IIntKeyedDataObject { }
    public partial class TblLearnerSessions : IntKeyedDataObject, IIntKeyedDataObject { }
    public partial class TblVars : IntKeyedDataObject, IIntKeyedDataObject { }
    public partial class TblVarsInteractions : IntKeyedDataObject, IIntKeyedDataObject { }
    public partial class TblVarsInteractionObjectives : IntKeyedDataObject, IIntKeyedDataObject { }
    public partial class TblVarsInteractionCorrectResponses : IntKeyedDataObject, IIntKeyedDataObject { }
    public partial class TblVarsScore : IntKeyedDataObject, IIntKeyedDataObject { }

    [ManyToManyRelationship(typeof(TblUsers), typeof(FxRoles))]
    public partial class RelUserRoles : RelTable, IRelationshipTable
    {
    }

    [ManyToManyRelationship(typeof(TblUsers), typeof(TblGroups))]
    public partial class RelUserGroups : RelTable, IRelationshipTable
    {
    }
    /*
    [ManyToManyRelationship(typeof(TblResources), typeof(TblResources))]
    public partial class RelResourcesDependency : RelTable, IRelationshipTable
    {
    }
     * */

    [DBEnum("fxRoles")]
    public enum FX_ROLE
    {
        STUDENT,
        LECTOR,
        TRAINER,
        ADMIN,
        SUPER_ADMIN
    }
}
