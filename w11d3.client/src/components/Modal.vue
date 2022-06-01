<template>
    <div class="modal" data-bs-backdrop="static" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="d-flex flex-column">
                        <h5 class="modal-title me-5">{{profile?.name}}</h5>
                        <button v-if="loggedIn && userFollowing" class="btn btn-danger" @click="unfollow">Unfollow</button>
                        <button v-else-if="loggedIn" class="btn btn-primary" @click="follow">Follow</button>
                    </div>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" @click="resetActive"></button>
                </div>
                <div class="modal-body">
                    <div class="d-flex flex-column">
                        <p>Followers</p>
                        <div>
                            <img v-for="ers in followers" :key="ers.id" :src="ers.picture" class="follow-img"/>
                            <h5 v-if="followers.length <= 0" class="text-center">No Followers</h5>
                        </div>
                        <hr />
                        <p>Following</p>
                        <div>
                            <img v-for="ings in followings" :key="ings.id" :src="ings.picture" class="follow-img"/>
                            <h5 v-if="followings.length <= 0" class="text-center">No Followings</h5>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js'
import { followsService } from '../services/FollowsService.js';
export default
{
    props:
    {
        profile:
        {
            type: Object,
            required: true
        }
    },

    setup(props)
    {
        return {
            loggedIn: computed(() => AppState.user.isAuthenticated),
            followers: computed(() => AppState.activeProfileFollowers),
            followings: computed(() => AppState.activeProfileFollowings),
            userFollowing: computed(() => AppState.userFollowings?.find(f => f.following === props.profile.id)),
            resetActive()
            {
                AppState.activeProfile = null;
                AppState.activeProfileFollowers = null;
                AppState.activeProfilesFollowings = null;
            },
            follow()
            {
                followsService.follow(props.profile.id);
            },
            unfollow()
            {
                followsService.unfollow(props.profile.id);
            }
        }
    }
}
</script>

<style lang="scss" scoped>
.follow-img
{
    height: 3rem;
    width: 3rem;
    border-radius: 50%;
    border: 1.1px solid black;
}
</style>