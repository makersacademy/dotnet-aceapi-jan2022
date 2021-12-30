## Investigation Exercises

For diagrams, use something other people can see and refer to rather than a bit
of paper. For example, [draw.io](https://draw.io/) or
[miro.com](https://miro.com/).

### Challenge 1

Use `curl` or [Postman](https://www.postman.com/) to:

* Sign up with a new account
* Create a new post
* Retrieve that post

### Challenge 2

Create a diagram of what happens when the user creates a new post.

### Challenge 3

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
