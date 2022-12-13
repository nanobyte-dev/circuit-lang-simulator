module Adder(a, b, cin) -> r, cout:
	r, cout = truth_table(a, b, cin) {
		0, 0, 0 : 0, 0
		0, 0, 1 : 1, 0
		0, 1, 0 : 1, 0
		0, 1, 1 : 0, 1
		1, 0, 0 : 1, 0
		1, 0, 1 : 0, 1
		1, 1, 0 : 0, 1
		1, 1, 1 : 1, 1
	}

module Adder4(a[4], b[4], cin) -> r[4], cout:
	var c[3]
	r[0], c[0] = Adder(a[0], b[0], cin)
	r[1], c[1] = Adder(a[1], b[1], c[0])
	r[2], c[2] = Adder(a[2], b[2], c[1])
	r[3], cout = Adder(a[3], b[3], c[2])

main module AndModule(a, b) -> r:
	r = a & b