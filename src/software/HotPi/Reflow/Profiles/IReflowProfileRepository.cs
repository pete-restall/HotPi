using System;
using System.Collections.Generic;

namespace Restall.HotPi.Reflow.Profiles
{
	public interface IReflowProfileRepository
	{
		bool TryGetById(Guid id, out ReflowProfile profile);

		IEnumerable<ReflowProfile> GetAll();
	}
}
