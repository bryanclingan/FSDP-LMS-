using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DATA.EF
{
    #region LessonViewsMetadata
    public class LessonViewMetaData
    {
        [Required(ErrorMessage ="*Required")]
        public string UserID { get; set; }
        [Required(ErrorMessage = "*Required")]
        public int LessonID { get; set; }
        [Required(ErrorMessage = "*Required")]
        public System.DateTime DateViewed { get; set; }
    }
    [MetadataType(typeof(LessonViewMetaData))]
    public partial class LessonView
    {

    }
    #endregion
    #region LessonsMetaData
    public class LessonMetaData
    {
        [Required(ErrorMessage = "*Required")]
        [MaxLength(200,ErrorMessage ="*Must be 200 characters or less")]
        [Display(Name ="Lesson Title")]
        public string LessonTitle { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name ="Course")]
        public int CourseID { get; set; }
        [MaxLength(250, ErrorMessage = "*Must be 250 characters or less")]
        public string Introduction { get; set; }
        [MaxLength(250, ErrorMessage = "*Must be 250 characters or less")]
        [Display(Name = "Video URL")]
        public string VideoURL { get; set; }
        [MaxLength(100, ErrorMessage = "*Must be 100 characters or less")]
        [Display(Name = "PDF")]
        public string PdfFileName { get; set; }
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
    [MetadataType(typeof(LessonMetaData))]
    public partial class Lesson { }
    #endregion
    #region UserDetailsMetaData
    public class UserDetailsMetaData
    {
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "*Must be 50 characters or less")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "*Must be 50 characters or less")]
        [Required(ErrorMessage = "*Required")]
        public string LastName { get; set; }
    }
    [MetadataType(typeof(UserDetailsMetaData))]
    public partial class UserDetail { }
    #endregion
    #region CourseCompletionsMetadata
    public class CourseCompletionMetadata
    {
        [Required(ErrorMessage = "*Required")]
        public string UserID { get; set; }
        [Required(ErrorMessage = "*Required")]
        public int CourseID { get; set; }
        [Required(ErrorMessage = "*Required")]
        public System.DateTime DateCompleted { get; set; }
    }
    [MetadataType(typeof(CourseCompletionMetadata))]
    public partial class CourseCompletion { }
    #endregion
    #region CoursesMetadata
    public class CourseMetaData
    {
        [MaxLength(200, ErrorMessage = "*Must be 200 characters or less")]
        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "*Required")]
        public string CourseName { get; set; }
        [MaxLength(500, ErrorMessage = "*Must be 500 characters or less")]
        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; }
        [Display(Name = "Active")]
        [Required(ErrorMessage = "*Required")]
        public bool IsActive { get; set; }
    }
    [MetadataType(typeof(CourseMetaData))]
    public partial class Cours { }
    #endregion
}
