using System.Collections.Generic;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.Models.Storages
{
    public interface IBanStorage
    {
        void AttachComputerToRoom(Computer computer, Room room);
        void DetachComputer(Computer computer);
        void BanComputer(Computer computer);
        void UnbanComputer(Computer computer);
        void EditComputer(string ip, bool banned, string currentUser);
        void BanRoom(Room room);
        void UnbanRoom(Room room);
        
        Computer GetComputer(string compAddress);
        Room GetRoom(string name);
        Room GetRoom(int id);
        Room GetRoom(Computer computer);
        RoomAttachment GetAttachment(string computerIp);
        RoomAttachment GetAttachment(Computer computer);
        IEnumerable<Computer> GetComputers();
        IEnumerable<Room> GetRooms();
        IEnumerable<Computer> ComputersAttachedToRoom(Room room);
        IEnumerable<RoomAttachment> GetRoomAttachments(); 
        
        void CreateComputer(Computer computer);
        void CreateRoom(Room room);
        void DeleteComputer(Computer computer);
        void DeleteRoom(Room room);
        bool IfBanned(string compAddress);
    }
}
