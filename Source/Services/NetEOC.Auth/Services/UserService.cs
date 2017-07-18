using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetEOC.Auth.Models;
using NetEOC.Auth.Data;

namespace NetEOC.Auth.Services
{
    public class UserService
    {
        public UserRepository UserRepository { get; set; }

        public OrganizationRepository OrganizationRepository { get; set; }

        public OrganizationMemberRepository OrganizationMemberRepository { get; set; }

        public OrganizationAdminRepository OrganizationAdminRepository { get; set; }

        public UserService()
        {
            UserRepository = new UserRepository();

            OrganizationRepository = new OrganizationRepository();

            OrganizationAdminRepository = new OrganizationAdminRepository();

            OrganizationMemberRepository = new OrganizationMemberRepository();
        }

        public async Task<User> Create(User user)
        {
            User existing;

            if(user.Id != Guid.Empty)
            {
                existing = await GetById(user.Id);

                if(existing == null)
                {
                    return await UserRepository.Create(user);
                }
            }

            existing = await GetByAuthId(user.AuthId);

            if (existing == null)
            {
                return await UserRepository.Create(user);
            }

            user.Id = existing.Id;

            return await Update(user);
        }

        public async Task<bool> Delete(Guid userId)
        {
            return await UserRepository.Delete(userId);
        }

        public async Task<User> GetByAuthId(string id)
        {
            return await UserRepository.GetByAuthId(id);
        }

        public async Task<User> GetById(Guid id)
        {
            return await UserRepository.Get(id);
        }

        public async Task<User> Update(User user)
        {
            if(user.Id == Guid.Empty || string.IsNullOrWhiteSpace(user.AuthId))
            {
                return null;
            }

            //merge user

            User existing = await GetById(user.Id);

            if (existing == null) return null;

            if (!string.IsNullOrWhiteSpace(user.Email)) existing.Email = user.Email;

            if (!string.IsNullOrWhiteSpace(user.Name)) existing.Name = user.Name;

            if (!string.IsNullOrWhiteSpace(user.Nickname)) existing.Nickname = user.Nickname;

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber)) existing.PhoneNumber = user.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(user.SmsNumber)) existing.SmsNumber = user.SmsNumber;

            if (!string.IsNullOrWhiteSpace(user.Picture)) existing.Picture = user.Picture;

            if(user.GeoPosition != null)
            {
                if (existing.GeoPosition == null) existing.GeoPosition = new GeoPosition();
                if (!string.IsNullOrWhiteSpace(user.GeoPosition.Accuracy)) existing.GeoPosition.Accuracy = user.GeoPosition.Accuracy;
                if (!string.IsNullOrWhiteSpace(user.GeoPosition.Altitude)) existing.GeoPosition.Altitude = user.GeoPosition.Altitude;
                if (!string.IsNullOrWhiteSpace(user.GeoPosition.AltitudeAccuracy)) existing.GeoPosition.AltitudeAccuracy = user.GeoPosition.AltitudeAccuracy;
                if (!string.IsNullOrWhiteSpace(user.GeoPosition.Heading)) existing.GeoPosition.Heading = user.GeoPosition.Heading;
                if (!string.IsNullOrWhiteSpace(user.GeoPosition.Latitude)) existing.GeoPosition.Latitude = user.GeoPosition.Latitude;
                if (!string.IsNullOrWhiteSpace(user.GeoPosition.Longitude)) existing.GeoPosition.Longitude = user.GeoPosition.Longitude;
                if (!string.IsNullOrWhiteSpace(user.GeoPosition.Speed)) existing.GeoPosition.Speed = user.GeoPosition.Speed;
            }

            if(user.Data != null)
            {
                foreach(var kv in user.Data)
                {
                    if (existing.Data.ContainsKey(kv.Key))
                    {
                        existing.Data[kv.Key] = kv.Value;
                    }else
                    {
                        existing.Data.Add(kv.Key, kv.Value);
                    }
                }
            }

            return await UserRepository.Update(existing);
        }

        public async Task<Guid[]> GetUserOrganizations(Guid userId)
        {
            Guid[] memberships = (await OrganizationMemberRepository.GetByUserId(userId)).Select(x => x.OrganizationId).ToArray();

            Guid[] administrations = (await OrganizationAdminRepository.GetByUserId(userId)).Select(x => x.OrganizationId).ToArray();

            Guid[] ownerships = (await OrganizationRepository.GetByOwnerId(userId)).Select(x => x.Id).ToArray();

            return memberships.Concat(administrations).Concat(ownerships).Distinct().ToArray();
        }
    }
}
