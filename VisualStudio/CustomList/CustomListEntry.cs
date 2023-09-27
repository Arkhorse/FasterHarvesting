using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace FasterHarvesting.CustomList
{
	public class ICustomListEntry
	{
		[JsonInclude]
		public virtual string ObjectName { get; set; } = string.Empty;
		[JsonInclude]
		public virtual float ObjectBreakDownTime { get; set; }
		[JsonInclude]
		public virtual float ObjectBreakDownTimeOriginal { get; set; }
		[JsonInclude]
		public virtual CustomDuration? m_CustomDuration { get; set; }
		[JsonInclude]
		public virtual List<string> UsableToolsOriginal { get; set; }

		// this is used for json deserialization only
		public ICustomListEntry() { }

		/// <summary>
		/// Creates a new instance of this class
		/// </summary>
		/// <param name="ObjectName">The exact name, case sensitive, of the prefab you want to target. Without the <c>.prefab</c> extension</param>
		/// <param name="ObjectBreakDownTime">The amount of time, in hours, for how long the break down is done. (90 minutes is <c>1.5f</c>)</param>
		/// <param name="ObjectBreakDownTimeOriginal">This is the original breakdown time. Used to reset things</param>
		public ICustomListEntry( string ObjectName, float ObjectBreakDownTime, float ObjectBreakDownTimeOriginal )
		{
			this.ObjectName = ObjectName;
			this.ObjectBreakDownTime = ObjectBreakDownTime;
			this.ObjectBreakDownTimeOriginal = ObjectBreakDownTimeOriginal;
		}

		/// <summary>
		/// Creates a new instance of this class
		/// </summary>
		/// <param name="ObjectName">The exact name, case sensitive, of the prefab you want to target. Without the <c>.prefab</c> extension</param>
		/// <param name="ObjectBreakDownTime">The amount of time, in hours, for how long the break down is done. (90 minutes is <c>1.5f</c>)</param>
		/// <param name="ObjectBreakDownTimeOriginal">This is the original breakdown time. Used to reset things</param>
		/// <param name="m_CustomDuration">If you want to use a custom duration, create a new instance of <see cref="CustomDuration"/></param>
		public ICustomListEntry(string ObjectName, float ObjectBreakDownTime, float ObjectBreakDownTimeOriginal, CustomDuration? m_CustomDuration = null)
		{
			this.ObjectName						= ObjectName;
			this.ObjectBreakDownTime			= ObjectBreakDownTime;
			this.ObjectBreakDownTimeOriginal	= ObjectBreakDownTimeOriginal;
			this.m_CustomDuration				= m_CustomDuration;
		}

		public ICustomListEntry(string ObjectName, float ObjectBreakDownTime, float ObjectBreakDownTimeOriginal, List<string> UsableToolsOriginal)
		{
			this.ObjectName						= ObjectName;
			this.ObjectBreakDownTime			= ObjectBreakDownTime;
			this.ObjectBreakDownTimeOriginal	= ObjectBreakDownTimeOriginal;
			this.UsableToolsOriginal			= UsableToolsOriginal;
		}

		public ICustomListEntry(string ObjectName,
						  float ObjectBreakDownTime,
						  float ObjectBreakDownTimeOriginal,
						  List<string> UsableToolsOriginal,
						  CustomDuration? m_CustomDuration = null)
		{
			this.ObjectName						= ObjectName;
			this.ObjectBreakDownTime			= ObjectBreakDownTime;
			this.ObjectBreakDownTimeOriginal	= ObjectBreakDownTimeOriginal;
			this.UsableToolsOriginal			= UsableToolsOriginal;
			this.m_CustomDuration				= m_CustomDuration;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();

			stringBuilder.Append( this.ObjectName );
			stringBuilder.Append( ',' );
			stringBuilder.Append( this.ObjectBreakDownTime.ToString( "N2", CultureInfo.InvariantCulture ) );
			stringBuilder.Append( ',' );
			stringBuilder.Append( this.ObjectBreakDownTimeOriginal.ToString( "N2", CultureInfo.InvariantCulture ) );

			if ( m_CustomDuration != null )
			{
				stringBuilder.Append(m_CustomDuration.ToString());
			}

			return stringBuilder.ToString();
		}
	}
}
