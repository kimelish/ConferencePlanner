using System.Linq;
using ConferenceDTO;
using Attendee = BackEnd.Data.Attendee;
using Session = BackEnd.Data.Session;
using Speaker = BackEnd.Data.Speaker;

namespace BackEnd.Infrastructure
{
    public static class EntityExtensions
    {
        public static SessionResponse MapSessionResponse(this Session session)
        {
            return new SessionResponse
            {
                Id = session.Id,
                Title = session.Title,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Speakers = session.SessionSpeakers?
                    .Select(ss => new ConferenceDTO.Speaker
                    {
                        Id = ss.SpeakerId,
                        Name = ss.Speaker.Name
                    })
                    .ToList(),
                TrackId = session.TrackId,
                Track = new Track
                {
                    Id = session?.TrackId ?? 0,
                    Name = session.Track?.Name
                },
                Abstract = session.Abstract
            };
        }

        public static SpeakerResponse MapSpeakerResponse(this Speaker speaker)
        {
            return new SpeakerResponse
            {
                Id = speaker.Id,
                Name = speaker.Name,
                Bio = speaker.Bio,
                WebSite = speaker.WebSite,
                Sessions = speaker.SessionSpeakers?
                    .Select(ss =>
                        new ConferenceDTO.Session
                        {
                            Id = ss.SessionId,
                            Title = ss.Session.Title
                        })
                    .ToList()
            };
        }

        public static AttendeeResponse MapAttendeeResponse(this Attendee attendee)
        {
            return new AttendeeResponse
            {
                Id = attendee.Id,
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                UserName = attendee.UserName,
                Sessions = attendee.SessionsAttendees?
                    .Select(sa =>
                        new ConferenceDTO.Session
                        {
                            Id = sa.SessionId,
                            Title = sa.Session.Title,
                            StartTime = sa.Session.StartTime,
                            EndTime = sa.Session.EndTime
                        })
                    .ToList()
            };
        }
    }
}