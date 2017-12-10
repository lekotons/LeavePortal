using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeavePortal.Web.Models
{
    public class LeaveCreationViewModel
    {
        [Display(Name = "From Date")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Please select the date")]
        public DateTime FromDate { get; set; }

        [Display(Name = "To Date")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Please select the date")]
        public DateTime ToDate { get; set; }

        [Display(Name = "Leave Requested In Days")]
        public decimal LeaveRequestedInDays { get; set; }

        [Display(Name = "Leave Balance In Days")]
        public decimal LeaveBalanceInDays { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>
        /// The balance.
        /// </value>
        public decimal Balance { get; set; }

        [Display(Name = "Leave Type")]
        [Required(ErrorMessage = "Please select the leave type")]
        public int LeaveTypeId { get; set; }

        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is edit mode.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is edit mode; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool IsEditMode { get; set; }

        public SelectListItem[] LeaveTypes { get; set; }
    }
}