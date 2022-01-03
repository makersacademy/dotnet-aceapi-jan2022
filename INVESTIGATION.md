## Investigation Exercises

For diagrams, use something other people can see and refer to rather than a bit
of paper. For example, [draw.io](https://draw.io/) or
[miro.com](https://miro.com/).

### Challenge 1

Get the project set up using the instructions in the [README](./README.md).

When you've done this, you should be able to see the Swagger API explorer.

### Challenge 2

Use the API explorer, [Postman](https://www.postman.com/), or `curl` to:

* Sign up with a new account
* Create a new post
* Retrieve that post

[This video should get you started.](https://youtu.be/zrS_x2K2w7o)

Note, if you use 'curl' you'll need to pass the `--insecure` argument to ignore
the errors caused by .NET not having a valid certificate (because it is a local
dev server).

### Challenge 3

Create a diagram of what happens when the user creates a new post.

### Challenge 4

_Note: You won't actually add this to the deployed application — this is just for
investigation purposes._

Make a change to the app so that:

* Posts have a new field on them called 'cool'.
* The app can create posts with `cool` set to true or false.
* The app responds to `GET /api/posts/cool` with a list of only the cool
  posts.
* The app responds to `GET /api/posts/uncool` with a list of only the uncool
  posts.

Include tests.
