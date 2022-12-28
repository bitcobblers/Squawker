﻿using GrainInterfaces.Model.Index;
using GrainInterfaces.States;

namespace GrainInterfaces.Tags
{
    public interface IHashTagGrain : IGrainWithStringKey
    {
        Task<HashTagLink> Link(Post post);
        Task<Guid[]> Posts();
    }
}