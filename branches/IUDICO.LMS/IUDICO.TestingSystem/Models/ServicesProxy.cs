using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;

namespace IUDICO.TestingSystem.Models
{
    /// <summary>
    /// Singleton Proxy holds reference to Lms Service.
    /// And provides easier access to other services.
    /// </summary>
    public class ServicesProxy
    {
        #region Service Properties

        public ILmsService LmsService { get; protected set; }

        public ICourseService CourseService
        {
            get
            {
                if (LmsService == null)
                {
                    throw new NullReferenceException("You should initialize LMSService first!");
                }
                return LmsService.FindService<ICourseService>();
            }
        }

        public ICurriculumService CurriculumService
        {
            get
            {
                if (LmsService == null)
                {
                    throw new NullReferenceException("You should initialize LMSService first!");
                }
                return LmsService.FindService<ICurriculumService>();
            }
        }

        public ISearchService SearchService
        {
            get
            {
                if (LmsService == null)
                {
                    throw new NullReferenceException("You should initialize LMSService first!");
                }
                return LmsService.FindService<ISearchService>();
            }
        }

        public IStatisticsService StatisticsService
        {
            get
            {
                if (LmsService == null)
                {
                    throw new NullReferenceException("You should initialize LMSService first!");
                }
                return LmsService.FindService<IStatisticsService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                if (LmsService == null)
                {
                    throw new NullReferenceException("You should initialize LMSService first!");
                }
                return LmsService.FindService<IUserService>();
            }
        }
        
        #endregion

        #region Singleton Implementation

        private static ServicesProxy _Instance;
        public static ServicesProxy Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ServicesProxy();
                }
                return _Instance;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Make constructor by default private to implement singleton pattern.
        /// </summary>
        private ServicesProxy()
        {
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes Lms Service property with appropriate service value
        /// which allows in further receive other services.
        /// </summary>
        /// <param name="service">ILmsService represents Lms Service.</param>
        public void Initialize(ILmsService service)
        {
            this.LmsService = service;
        }

        #endregion
    }
}