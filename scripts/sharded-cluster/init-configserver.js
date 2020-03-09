rs.initiate(
   {
      _id: "rs-config-server",
      configsvr: true,
      version: 1,
      members: [
         { _id: 0, host : "host1:27118" },
         { _id: 1, host : "host2:27118" },
         { _id: 2, host : "host3:27118" }
      ]
   }
)
