﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Nandel.StikyNotes.Core.Entities;
using Nandel.StikyNotes.Core.Repositories;
using Nandel.StikyNotes.Core.Services;

namespace Nandel.StikyNotes.Application.Commands.SetHttpGet
{
    public class SetHttpGetHandler : IRequestHandler<SetHttpGetCommand>
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IValidator<HttpGet> _validator;
        private readonly ILogger<SetHttpGetHandler> _logger;

        public SetHttpGetHandler(IMediaRepository mediaRepository, IValidator<HttpGet> validator, ILogger<SetHttpGetHandler> logger)
        {
            _mediaRepository = mediaRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Unit> Handle(SetHttpGetCommand request, CancellationToken cancellationToken)
        {
            var entity = new HttpGet()
            {
                Key = request.Key,
                Address = request.Address,
                FieldToDisplay = request.FieldToDisplay
            };

            await _validator.ValidateAsync(entity);
            await _mediaRepository.AddAsync(entity);
    
            return Unit.Value;
        }
    }
}