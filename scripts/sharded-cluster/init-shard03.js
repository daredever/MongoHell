rs.initiate(
   {
      _id: "rs-shard-03",
      version: 1,
      members: [
         { _id: 0, host : "host1:27121" },
         { _id: 1, host : "host2:27121" },
         { _id: 2, host : "host3:27121" }
      ]
   }
)
