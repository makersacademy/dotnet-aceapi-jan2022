# Tickets

## Phase One: Enhancements

Starting with small tickets is a great way to get used a codebase and build up
your mental picture of how it fits together. In the real world, you'll likely
start your jobs this way.

For each ticket, include tests and documentation updates.

* **Order posts in reverse chronological order.**  
  Right now posts are retrieved in what seems to be chronological order. Change
  this so they arrive in reverse chronological order.

* **Add tests to cover the case where the Post's Body is blank.**

* **Add tests to cover the case where the user logs in with the wrong
  password.**

* **Add tests to cover the case where the user logs in with a username for which
  there is no account.**

* **Implement continuous integration in the deployment pipeline.**  
  Right now, the Github Actions pipeline deploys the project no matter if the
  tests pass or fail. This is quite risky, potentially allowing foreseeable
  problems to reach users. Ensure the tests run in the CI pipeline and only
  deploy the project if the tests pass.

## Phase Two: Features

Implement the following two user stories.

Ensure you include tests and documentation updates.

### Comments

> As a user  
> So that I can engage in a discussion  
> I want to be able to leave comments on posts

### Upvotes and downvotes

> As a user  
> So that I can promote or bury posts I like or dislike
> I want to be able to upvote and downvote posts

## Phase Three: Cloud-enabled Features

Implement the following user stories.

Ensure you include tests and documentation updates. Remember that this system is
deployed to Azure Cloud and you'll need to figure out how to implement it in
that context.

> As a user  
> So that I can share my photos with others
> I want to be able to upload photos to the main feed

> As a user
> So that I don't use all my bandwidth
> I want photos in the feed to be an appropriate size

For this last feature, the 'appropriate size' may be different per device.
You'll need to serve some JSON a bit like this:

```json
{
  // ...
  photoUrls: {
    small: "https://example.org/storage/photo-640.png",
    medium: "https://example.org/storage/photo-1280.png",
    large: "https://example.org/storage/photo-2560.png",
  }
  // ...
}
```
