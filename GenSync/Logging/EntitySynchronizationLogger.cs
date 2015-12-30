﻿// This file is Part of CalDavSynchronizer (http://outlookcaldavsynchronizer.sourceforge.net/)
// Copyright (c) 2015 Gerhard Zehetbauer
// Copyright (c) 2015 Alexander Nimmervoll
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

namespace GenSync.Logging
{
  public class EntitySynchronizationLogger : IEntitySynchronizationLogger
  {
    private readonly List<string> _mappingErrors = new List<string>();
    private string _aId;
    private string _bId;
    private string _exceptionThatLeadToAbortion;

    public event EventHandler Disposed;

     void OnDisposed ()
    {
      var handler = Disposed;
      if (handler != null)
        handler (this, EventArgs.Empty);
    }

    public EntitySynchronizationLogger ()
    {
    }

    public void LogMappingError (Exception exception)
    {
      _mappingErrors.Add (exception.ToString());
    }

    public void SetAId (object id)
    {
      _aId = id.ToString();
    }

    public void SetBId (object id)
    {
      _bId = id.ToString();
    }

    public void LogAbortedDueToError (Exception exception)
    {
      _exceptionThatLeadToAbortion = exception.ToString();
    }

    public EntitySynchronizationReport GetReport ()
    {
      return new EntitySynchronizationReport()
             {
                 AId = _aId,
                 BId = _bId,
                 ExceptionThatLeadToAbortion = _exceptionThatLeadToAbortion,
                 MappingErrors = _mappingErrors.ToArray()
             };
    }

    public void Clear ()
    {
      _aId = string.Empty;
      _bId = string.Empty;
      _exceptionThatLeadToAbortion = String.Empty;
      _mappingErrors.Clear();
    }

    public bool HasErrors
    {
      get { return _mappingErrors.Count > 0 || !string.IsNullOrEmpty (_exceptionThatLeadToAbortion); }
    }

    public void Dispose ()
    {
      OnDisposed();
    }
  }
}