# Comparing Network Performance Between gRPC and GraphQL

Class project for Computer Networks - COSC 6377 (Spring 2020)

## What am I planning to do?

For this project, I plan to compare network performance between two competing, though not mutually exclusive, modern APIs.  While network performance and latency will be used as the primary criteria for comparing the two on a technical level, other metrics such as ease of use, testing and debugging will help to clarify gRPC and GraphQL’s different design approaches.  In order to provide useful data, I will use some company networking devices to create a simple network that can simulate a variety of network conditions.

## Why is this technology important?

Google’s gRPC and Facebook’s GraphQL represent two of the most popular modern APIs and are being widely adopted because of their different approaches to the RESTful API paradigm.  While gRPC favors small and efficient serialization of protocol buffers (“protobufs”) to provide an API reminiscent of RPC, GraphQL makes it easier for the client to choose what data to fetch from the serve’s single endpoint.  Today’s prevalence of connected devices, including mobile and IoT, requires a newer API paradigm that is no longer beholden to the RESTful client-server relationship, but can handle small data transfers and push notifications.

## What networking topics do I expect to learn?

I expect to have a decent understanding of how gRPC and GraphQL work, not just in how they are implemented on a programming level, but also how they send data across networks.  For example, are some of gRPC’s latency benefits a side-effect of keeping a connection alive, as opposed to GraphQL?  I would also like to familiarize myself with GraphQL’s schema and query language, especially since it seems so focused towards giving the client as much freedom to choose which data to fetch from the server.  
## What deliverables or milestones do I propose?

1. Mid-term milestones
-       gRPC implementation done
-       GraphQL implementation done
-       Preliminary steps for gathering data, including network setup

2. Final milestone
-       Data gathering done
-       Multiple network conditions tested and debugged
