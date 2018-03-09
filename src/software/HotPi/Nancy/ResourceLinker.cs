﻿using System;
using Nancy;
using Nancy.Linker;

namespace Restall.HotPi.Nancy
{
	public class ResourceLinker
	{
		private readonly NancyContext context;
		private readonly IResourceLinker linker;

		public ResourceLinker(NancyContext context, IResourceLinker linker)
		{
			this.context = context;
			this.linker = linker;
		}

		public Uri RelativeUriFor(string routeName)
		{
			return this.linker.BuildRelativeUri(this.context, routeName);
		}
	}
}