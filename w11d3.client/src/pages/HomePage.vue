<template>
  <div class="container-fluid mt-4">
      <div class="row">
          <div class="col-12 col-md-3" v-for="p in profiles" :key="p.id">
              <ProfileCard :profile="p" />
          </div>
      </div>
  </div>
  <Modal id="profile-modal" :profile="activeProfile" />
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { profilesService } from "../services/ProfilesService.js";
import { AppState } from '../AppState.js';
import { Modal } from 'bootstrap';
import { followsService } from '../services/FollowsService.js';
export default {
  name: 'Home',
    watch:
    {
        async activeProfile(newProfile)
        {
            if(newProfile != null)
            {
                AppState.activeProfileFollowers = await followsService.getFollowers(newProfile.id);
                AppState.activeProfileFollowings = await followsService.getFollowings(newProfile.id);
                Modal.getOrCreateInstance(document.getElementById("profile-modal")).show();
            }
        }
    },

  setup()
  {
      onMounted(async () => {
          try
          {
              await profilesService.getAll();
          }
          catch(error)
          {
              logger.error("[HomePage.vue > onMounted]", error.message);
              Pop.toast(error.message, "error");
          }
      })
      return {
          profiles: computed(() => AppState.profiles),
          activeProfile: computed(() => AppState.activeProfile)
      };
  }
}
</script>

<style scoped lang="scss">
</style>
