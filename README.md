Ticket System Implementation
I Create The web Application using
- Tool: Visual Studio 2013
- .Net: aps.net
- Language: C#
- Database: SQL Server 2005

I: Structure Database
  +tblTicketTracking
    -id
    -status_id
    -title
    -description
    -create_by
    -create_date
    -severity_id
    -pririty_id
    -resolved_by
    -resolved_date
    -resolved_description
    -is_active
  
  +tblTicketType
    -id
    -name
    -description
    -create_by
    -create_date
    -is_active
    
  +tblUserGroup
    -id
    -name
    -is_active
(this table have 4 row: QA, RD, PM, Administrator)     
  
  +tblUser
    -id
    -first_name
    -last_name
    -sex
    -user_name
    -password
    -description
    -usertype_id
    -is_active
    
  +tblStaus
    -id
    -name
    -is_active
(this table have 2 row: Pending, Resolve)

  +tblSeverity
    -id
    -name
    -is_active
(this table have 4 row: Critical, Major, Minor, Low)

  +tblProperty
    -id
    -name
    -is_active
(this table have 4 row: Immediate, High, Medium, Low)    
    
II: Processing App
  *This App have 4 page (Ticket Tracking, Resolve Bug, New Ticke Type, Create User)
  
  -User QA can open page (Ticket Tracking) to create bug, edit bug , delete bug.
when user QA save data it save bug to tblTicketTracking and status bug is Pending.

  -User RD can open page (Resolve Bug) when user RD open page all bug (form user QA create) loag to the pag. and user RD click button Resolve and save, the data Update to tblTicketTracking and status bug is Resolve.
  
  -User PM can open page (New Ticke Type) to create New Ticket Type to table tblTicketType, new ticket type is new sub pag of (Ticket Tracking).
  
  -User Administrator can open page (Create User) to create New User to table tblUser, All user have permision by User Type (tblUserGroup).
  
  
III: URL for Tesing this App and all User Password
  -103.240.113.190:880
  -username:QA  paaword:123
  -username:RD  paaword:123
  -username:PM  paaword:123
  -username:Admin  paaword:123
  
  
  
  Create By: Kaing Teangka
  Thank You
  Best Regards,
  
    
  
    
