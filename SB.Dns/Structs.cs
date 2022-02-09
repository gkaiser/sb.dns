using System;
using System.Linq;

namespace SB.Dns
{
  internal static class Structs
  {
		struct DNS_HEADER
		{
			ushort id; // identification number

			byte rd = 1; // recursion desired
			byte tc = 1; // truncated message
			byte aa = 1; // authoritive answer
			byte opcode = 4; // purpose of message
			byte qr = 1; // query/response flag

			byte rcode = 4; // response code
			byte cd = 1; // checking disabled
			byte ad = 1; // authenticated data
			byte z = 1; // its z! reserved
			byte ra = 1; // recursion available

			ushort q_count; // number of question entries
			ushort ans_count; // number of answer entries
			ushort auth_count; // number of authority entries
			ushort add_count; // number of resource entries
		};

		//Constant sized fields of query structure
		struct QUESTION
		{
			ushort qtype;
			ushort qclass;
		};

		//Constant sized fields of the resource record structure
#pragma pack(push, 1)
		struct R_DATA
		{
			ushort type;
			ushort _class;
			uint ttl;
			ushort data_len;
		};
#pragma pack(pop)

		//Pointers to resource record contents
		struct RES_RECORD
		{
			byte[] name;
			R_DATA resource;
			byte[] rdata;
		};

		//Structure of a Query
		struct QUERY
		{
			byte[] name;
			QUESTION ques;
		}

  }
}
