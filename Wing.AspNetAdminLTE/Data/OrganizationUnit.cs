using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wing.AspNetAdminLTE.Data
{
	/// <summary>
	/// Represents an organization unit (OU).
	/// </summary>
	public class OrganizationUnit
	{
		/// <summary>
		/// Maximum length of the <see cref="DisplayName"/> property.
		/// </summary>
		public const int MaxDisplayNameLength = 128;

		/// <summary>
		/// Maximum depth of an UO hierarchy.
		/// </summary>
		public const int MaxDepth = 16;

		/// <summary>
		/// Length of a code unit between dots.
		/// </summary>
		public const int CodeUnitLength = 5;

		/// <summary>
		/// Maximum length of the <see cref="Code"/> property.
		/// </summary>
		public const int MaxCodeLength = MaxDepth * (CodeUnitLength + 1) - 1;

		/// <summary>
		/// Unique identifier for this entity.
		/// </summary>
		public virtual int Id { get; set; }

		/// <summary>
		/// Parent <see cref="OrganizationUnit"/>.
		/// Null, if this OU is root.
		/// </summary>
		[ForeignKey("ParentId")]
		public virtual OrganizationUnit Parent { get; set; }

		/// <summary>
		/// Parent <see cref="OrganizationUnit"/> Id.
		/// Null, if this OU is root.
		/// </summary>
		public virtual int? ParentId { get; set; }

		/// <summary>
		/// Hierarchical Code of this organization unit.
		/// Example: "00001.00042.00005".
		/// This is a unique code for a Tenant.
		/// It's changeable if OU hierarch is changed.
		/// </summary>
		[Required]
		[StringLength(MaxCodeLength)]
		public virtual string Code { get; set; }

		/// <summary>
		/// Display name of this role.
		/// </summary>
		[Required]
		[StringLength(MaxDisplayNameLength)]
		public virtual string DisplayName { get; set; }

		/// <summary>
		/// Children of this OU.
		/// </summary>
		public virtual ICollection<OrganizationUnit> Children { get; set; }
	}
}
