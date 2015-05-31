﻿using System;
using System.Collections.Generic;

namespace Inklewriter.Parsed
{
	public abstract class Object
	{
		public DebugMetadata debugMetadata { 
            get {
                if (_debugMetadata == null) {
                    if (parent != null) {
                        return parent.debugMetadata;
                    }
                }

                return _debugMetadata;
            }

            set {
                _debugMetadata = value;
            }
        }
        private DebugMetadata _debugMetadata;

		public Parsed.Object parent { get; set; }

		private Runtime.Object _runtimeObject;
		public Runtime.Object runtimeObject
		{
			get {
				if (_runtimeObject == null) {
					_runtimeObject = GenerateRuntimeObject ();
				}
				return _runtimeObject;
			}

			set {
				_runtimeObject = value;
			}
		}

		public abstract Runtime.Object GenerateRuntimeObject ();

        public virtual void ResolveReferences(Story context)
		{

		}

		public virtual void Error(string message, Parsed.Object source = null)
		{
			if (source == null) {
				source = this;
			}

			if (this.parent != null) {
				this.parent.Error (message, source);
			}
		}
	}
}

