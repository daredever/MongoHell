rs.initiate(
   {
      _id: "rs-shard-01",
      version: 1,
      members: [
         { _id: 0, host : "host1:27119" },
         { _id: 1, host : "host2:27119" },
         { _id: 2, host : "host3:27119" }
      ]
   }
)
