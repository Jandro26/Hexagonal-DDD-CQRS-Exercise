﻿using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.core.domain.commandBus
{
    public interface ICommandDispacher
    {
        Task Dispatch<T>(T command, CancellationToken cancellationToken) where T : Command;
    }
}
