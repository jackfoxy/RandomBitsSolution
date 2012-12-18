#<a name="randombits" class="anchor" href="#randombits"><span class="mini-icon mini-icon-link"></a>RandomBits

**RandomBits** is a F# library consuming the [ANU Quantum Random Numbers Server](http://qrng.anu.edu.au/index.php) API, providing the following end-products:

Signed and unsigned 8, 16, 32, and 64 bit random numbers.

Random numbers constrained within a range.

Sequences of random numbers.

Sequences of random numbers constrained within a range.

Sequences of random numbers constrained within a range and each member of the sequence is unique.

##<a name="purpose" class="anchor" href="#purpose"><span class="mini-icon mini-icon-link"></a>Purpose of RandomBits

Appropriate pseudo-random number generators are perfectly adequate for almost any random number generation use, and significantly faster than using **RandomBits**, especially becausej of network latency. **RandomBits** is primarily a demonstration project.

DISCLAIMER: what follows are scattered notes to myself. Don't try to read anything into the notes or the project itself, which is largely an engineering excercise and not anything useful.


{"type":"string","length":10,"size":10,"data":["9b","b2"],"success":true}

14,000 tcp sweet spot? - 96 header
{"type":"string","length":12,"size":12,"data":["6d35389de6eaa5b8c7d64e6e",
"2b1c574d583c7c2977161a40",
"ac2f26d207125de2c6dfebb2",
"9b65b8624723a9ac37428530",
"80ee9a6429903ba9825c48ee",
"a46f74ffa410049ec8b46600",
"36f7a6463382a3d01b718b8f",
"61b2acb5c284e89ef4a7df00",
"c3552f7749d60aa8688b4cc0",
"f0d10c26bf3548428d4d9ec0",
"03f14ebe7306734be66d8e53",
"f939ef85dbd282fb510c2c69"],"success":true}

wireshark tests
1	6.585	7.147		0.562
1	5.9538	6.5166		0.5628
1	2.5971	3.3208		0.7237
1	6.0997	6.6445		0.5448
1				
	7.042	7.5873		0.5453
2	1.9777	2.5227		0.545
2	2.9467	3.6963		0.7496
2	5.2516	5.7952		0.5436
2	6.035	6.5811		0.5461
2				
	5.27	5.8143		0.5443
3	6.6731	7.2266		0.5535
3	1.7057	2.2512		0.5455
3	7.6566	8.2019		0.5453
3	0.3551	1.0808		0.7257
3	2.9589	3.5065		0.5476
				
4	5.1614	5.7072		0.5458
				
10	8.3839	9.117		0.7331
10	7.3921	8.1226		0.7305
10	5.3005	6.0447		0.7442
10	5.3922	6.3266		0.9344
10	8.6668	9.3942		0.7274
				
20	4.0821	4.9939		0.9118
				
30	4.0857	5.1782		1.0925


timeoutHandler
invariant maxMsgQueueLength


https://github.com/panesofglass/FSharp.Reactive/blob/master/src/Observable.fs
http://www.random.org/
http://mikehadlow.blogspot.co.uk/2012/11/using-blockingcollection-to-communicate.html
http://msdn.microsoft.com/en-us/library/dd267265.aspx
http://qrng.anu.edu.au/FAQ.php


Read 4K randomly from SSD*              150,000   ns    0.15 ms
Read 1 MB sequentially from memory      250,000   ns    0.25 ms
Round trip within same datacenter       500,000   ns    0.5  ms
Read 1 MB sequentially from SSD*      1,000,000   ns    1    ms  4X memory
Disk seek                            10,000,000   ns   10    ms  20x datacenter roundtrip
Read 1 MB sequentially from disk     20,000,000   ns   20    ms  80x memory, 20X SSD
Send packet CA->Netherlands->CA     150,000,000   ns  150    ms

The ANU Quantum Random Numbers Server currently generates random bits at the rate of 5.7Gbits/s. The rate at which they can be streamed to the internet is limited by the bandwidth of the connection. Using Wireshark to profile my connection from California (which typically has a local (to the Bay Area) download connection bandwidth of 0.025Gbits/s, according to speedtest.net) to Australia found using the ANU API  I could download blocks containing 245,760 random bits in a one second request/response cycle, for an effective random bit download rate of 0.000246Gbits/s. (Speedtest rates my dowload connection to Canberra, Australia at 0.007Gbits/s.)

Lacking more specific information on how the server provisions response blocks it would seem multi-threaded requests could result in overlapping response blocks. Therefore requests are single-threaded. 