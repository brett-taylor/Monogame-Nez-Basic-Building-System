﻿using System;


namespace Nez.Analysis
{
	/// <summary>
	/// Stopwatch is used to measure the general performance of Silverlight functionality. Silverlight
	/// does not currently provide a high resolution timer as is available in many operating systems,
	/// so the resolution of this timer is limited to milliseconds. This class is best used to measure
	/// the relative performance of functions over many iterations.
	/// </summary>
	public sealed class Stopwatch
	{
		long _startTick;
		long _elapsed;
		bool _isRunning;


		/// <summary>
		/// Creates a new instance of the class and starts the watch immediately.
		/// </summary>
		/// <returns>An instance of Stopwatch, running.</returns>
		public static Stopwatch StartNew()
		{
			Stopwatch sw = new Stopwatch();
			sw.start();
			return sw;
		}


		/// <summary>
		/// Completely resets and deactivates the timer.
		/// </summary>
		public void reset()
		{
			_elapsed = 0;
			_isRunning = false;
			_startTick = 0;
		}


		/// <summary>
		/// Begins the timer.
		/// </summary>
		public void start()
		{
			if( !_isRunning )
			{
				_startTick = getCurrentTicks();
				_isRunning = true;
			}
		}


		/// <summary>
		/// Stops the current timer.
		/// </summary>
		public void stop()
		{
			if( _isRunning )
			{
				_elapsed += getCurrentTicks() - _startTick;
				_isRunning = false;
			}
		}


		/// <summary>
		/// Gets a value indicating whether the instance is currently recording.
		/// </summary>
		public bool isRunning
		{
			get { return _isRunning; }
		}


		/// <summary>
		/// Gets the Elapsed time as a Timespan.
		/// </summary>
		public TimeSpan elapsed
		{
			get { return TimeSpan.FromMilliseconds( elapsedMilliseconds ); }
		}


		/// <summary>
		/// Gets the Elapsed time as the total number of milliseconds.
		/// </summary>
		public long elapsedMilliseconds
		{
			get { return getCurrentElapsedTicks() / TimeSpan.TicksPerMillisecond; }
		}


		/// <summary>
		/// Gets the Elapsed time as the total number of ticks (which is faked
		/// as Silverlight doesn't have a way to get at the actual "Ticks")
		/// </summary>
		public long elapsedTicks
		{
			get { return getCurrentElapsedTicks(); }
		}


		long getCurrentElapsedTicks()
		{
			return (long)( this._elapsed + ( isRunning ? ( getCurrentTicks() - _startTick ) : 0 ) );
		}


		long getCurrentTicks()
		{
			// TickCount: Gets the number of milliseconds elapsed since the system started.
			return Environment.TickCount * TimeSpan.TicksPerMillisecond;
		}

	}
}
