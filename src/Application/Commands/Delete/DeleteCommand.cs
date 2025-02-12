﻿using MediatR;

namespace Nandel.StikyNotes.Application.Commands.Delete
{
    public record DeleteCommand : IRequest
    {
        public string Key { get; set; }

        public DeleteCommand()
        { }

        public DeleteCommand(string key)
        {
            Key = key;
        }
    }
}